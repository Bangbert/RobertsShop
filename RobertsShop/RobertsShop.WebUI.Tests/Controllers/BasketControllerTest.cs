using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobertsShop.Core.Contracts;
using RobertsShop.Core.Models;
using RobertsShop.Core.ViewModels;
using RobertsShop.Services;
using RobertsShop.WebUI.Controllers;
using RobertsShop.WebUI.Tests.Mocks;

namespace RobertsShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTest
    {
        [TestMethod]
        public void CanAddBasketItem()
        {

            //setup
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orders = new MockContext<Order>();
            IRepository<Customer> customers = new MockContext<Customer>();

            var httpContexT = new MockHttpContext();

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService, orderService, customers);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext
                (httpContexT, new System.Web.Routing.RouteData(), controller);
            //Act
            //basketService.AddToBasket(httpContexT, "1");

            controller.AddToBasket( "1");
            

            Basket basket = baskets.Collection().FirstOrDefault();

            //Assert
            Assert.IsNotNull(baskets);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);
        }
        [TestMethod]
        public void CanGetSommaryViewModel()
        {
            //setup
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();
            IRepository<Order> orders = new MockContext<Order>();
            IRepository<Customer> customers = new MockContext<Customer>();


            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);
            IOrderService orderService = new OrderService(orders);
           

            var controller = new BasketController(basketService, orderService, customers);

            var httpContexT = new MockHttpContext();
            
            httpContexT.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id});
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContexT, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(25.00m, basketSummary.BasketTotal);
        }
        [TestMethod]
        public void CanCheckoutAndCreateOrder()
        {
            IRepository<Product> products = new MockContext<Product>();
            products.Insert(new Product() { Id = "1", Price = 10.00m });
            products.Insert(new Product() { Id = "2", Price = 5.00m });

            IRepository<Customer> customers = new MockContext<Customer>();
            IRepository<Basket> baskets = new MockContext<Basket>();
            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2, BasketId = basket.Id });
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 1, BasketId = basket.Id });
            customers.Insert(new Customer() { Id = "1", Email = "RobertKirchner1@gmx.de", ZipCode = "50672" });

            IPrincipal faki = new GenericPrincipal(new GenericIdentity("RobertKirchner1@gmx.de", "Forms"), null);
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);

            IRepository<Customer> custoemrs = new MockContext<Customer>();
            IRepository<Order> orders = new MockContext<Order>();
            IOrderService orderService= new OrderService(orders);

            
            var controller = new BasketController(basketService, orderService, custoemrs);
            var httpContext = new MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket")
            {
                Value = basket.Id
            });
            httpContext.User = faki;

            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);


            //Act
            Order order = new Order();
            controller.Checkout(order);

            //assert
            Assert.AreEqual(2, order.OrderItems.Count);
            Assert.AreEqual(0, basket.BasketItems.Count);

            Order orderInRep = orders.Find(order.Id);
            Assert.AreEqual(2, orderInRep.OrderItems.Count);

        }
    }
}
