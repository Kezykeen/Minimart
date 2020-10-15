using MiniMart.Models;
using MiniMart.Persistence;
using MiniMart.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;

        public CheckOutController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            bool isValid = _unitOfWork.CheckOutRepo.CheckOrderValidity(id);

            // Verify if the Customer owns the order
            if (isValid && _unitOfWork.CheckOutRepo.GetOrder(id).Username == User.Identity.Name)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: CheckOut
        public ActionResult AddressAndPayment()
        {
            var cart = _unitOfWork.ShoppingCartRepo.GetCart(this.HttpContext);

            if (cart.GetCartItemCount() == 0)
            {
                return View("Error");
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

            //Process the order
            var cart = _unitOfWork.ShoppingCartRepo.GetCart(this.HttpContext);
            cart.CreateOrder(order);

            return RedirectToAction("Complete", new { id = order.OrderId });
        }
    }
}