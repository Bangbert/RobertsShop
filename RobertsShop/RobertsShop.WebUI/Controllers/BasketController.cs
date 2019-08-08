using RobertsShop.Core.Contracts;
using RobertsShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RobertsShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Customer> customers;
        IBasketService basketService;
        IOrderService orderService;

        public BasketController(IBasketService _basketService, IOrderService _orderService, IRepository<Customer> _customers)
        {
            customers = _customers;
            basketService = _basketService;
            orderService = _orderService;
        }

        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);

            return View(model);
        }
        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummery = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummery);
        }
        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    Surname = customer.LastName,
                    ZipCode = customer.ZipCode
                };
                return View(order);


            }
            else
            {
                return RedirectToAction("Error");
            }

        }
        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order _order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            _order.Orderstatus = "Order Created";
            _order.Email = User.Identity.Name;
            //Process payment

            _order.Orderstatus = "Payment Processed";
            orderService.CreateOrder(_order, basketItems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("ThankYou", new { OrderId = _order.Id });
           
        }

        public ActionResult ThankYou(string OrderId) {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}