using MiniMart.Models;
using MiniMart.Persistence;
using MiniMart.Repositories;
using MiniMart.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniMart.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View(new ShoppingCartViewModel
            {
                CartItems = _unitOfWork.ShoppingCartRepo.GetCart(this.HttpContext).GetCartItems(),
                CartTotal = _unitOfWork.ShoppingCartRepo.GetCart(this.HttpContext).GetTotal()
            });
        }

        public ActionResult AddToCart(int Id)
        {
            var addedProduct = _unitOfWork.ProductRepo.FindProduct(Id);

            _unitOfWork.ShoppingCartRepo.GetCart(this.HttpContext).AddToCart(addedProduct);

            return View();
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int Id)
        {
            string productName = _unitOfWork.ShoppingCartRepo.GetCartById(Id).Product.Name;

            return Json(new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) +
                    " has been removed from your shopping cart.",
                CartTotal = _unitOfWork.ShoppingCartRepo.GetCart(this.HttpContext).GetTotal(),
                CartCount = _unitOfWork.ShoppingCartRepo.GetCart(this.HttpContext).GetCount(),
                ItemCount = _unitOfWork.ShoppingCartRepo.GetCart(this.HttpContext).RemoveFromCart(Id),
                DeleteId = Id
            });
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            double result = _unitOfWork.ShoppingCartRepo.GetCart(this.HttpContext).GetCartItemCount();

            return PartialView("CartSummary", result);
        }

    }
}