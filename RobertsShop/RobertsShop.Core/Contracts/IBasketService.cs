using RobertsShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RobertsShop.Core.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase _httpContext, string _productId);
        void RemoveFromBasket(HttpContextBase _httpContext, string _itemId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase _httpContext);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase _httpContext);
        void ClearBasket(HttpContextBase _httpContext);
    }
}
