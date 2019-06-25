using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobertsShop.WebUI.Controllers;
using RobertsShop.Core.Contracts;
using RobertsShop.Core.Models;
using System.Web.Mvc;
using RobertsShop.Core.ViewModels;
using System.Linq;

namespace RobertsShop.WebUI.Tests.Controllers
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            IRepository<Product> productContext = new Mocks.MockContext<Product>();
            IRepository<ProductCategory> productCategorysContext = new Mocks.MockContext<ProductCategory>();
            HomeController controller = new HomeController(productContext, productCategorysContext);

            productContext.Insert(new Product());

            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;
            //ToDo Add Product for sucsessfull test

            Assert.AreEqual(1, viewModel.Products.Count());
        }
       
    }
}
