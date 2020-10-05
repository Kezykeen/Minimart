using MiniMart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniMart.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: CheckOut
        public ActionResult AddressAndPayment()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            double result = cart.GetCartItemCount();

            if (result == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            return View();
        }

        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            order.Username = User.Identity.Name;
            order.OrderDate = DateTime.Now;

            //Save Order
            db.Order.Add(order);
            db.SaveChanges();
            //Process the order
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);

            return RedirectToAction("Complete", new { id = order.OrderId });
        }

        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = db.Order.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}