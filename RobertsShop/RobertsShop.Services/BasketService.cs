using RobertsShop.Core.Contracts;
using RobertsShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RobertsShop.Services
{
    class BasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> _productContext,
        IRepository<Basket> _basketContext)
        {
         productContext = _productContext;
         basketContext = _basketContext;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket =  new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }
            return basket;

        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase _httpContext, string _productId)
        {
            Basket basket = GetBasket(_httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == _productId);

            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = _productId,
                    Quantity = 1
                };
                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity++;
            }
            //Entety Frameworks automatecly recognizes that item has changed andupdates database
            basketContext.Commit();


        }

        public void RemoveFromBasket(HttpContextBase _httpContext, string _itemId)
        {
            Basket basket = GetBasket(_httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == _itemId);


            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }


    }
}
