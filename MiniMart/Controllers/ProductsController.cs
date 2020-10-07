using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiniMart.Models;
using MiniMart.ViewModels;

namespace MiniMart.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //temp code browse
        public ActionResult Browse(string category)
        {
            var categoryModel = db.Category.Include("Products")
                .Single(c => c.Name == category);
            return View(categoryModel);
        }

        public ActionResult CategoryList()
        {
            var categories = db.Category.ToList();
            return PartialView("CategoryListPartialView", categories);
        }

        // GET: Products
        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.Category);

            if (User.IsInRole("CanManageProducts"))
                return View("AdminIndex", product.ToList());

            return View(product.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Product.Find(id);
            Category category = db.Category.Single(c => c.Id == product.CategoryId);

            ProductCategoryViewModel viewModel = new ProductCategoryViewModel
            {
                product = product,
                category = category
            };

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // GET: Products/Create
        [Authorize(Roles = RoleName.CanManageProducts)]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageProducts)]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,ArtUrl,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);

            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = RoleName.CanManageProducts)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Product.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageProducts)]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,ArtUrl,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = RoleName.CanManageProducts)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize(Roles = RoleName.CanManageProducts)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
