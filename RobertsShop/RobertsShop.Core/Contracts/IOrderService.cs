using RobertsShop.Core.Models;
using RobertsShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertsShop.Core.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(Order _baseOrder, List<BasketItemViewModel> basketItems);

    }
}
