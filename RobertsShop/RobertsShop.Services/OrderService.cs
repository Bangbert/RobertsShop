using RobertsShop.Core.Contracts;
using RobertsShop.Core.Models;
using RobertsShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertsShop.Services
{
    public class OrderService :IOrderService
    {
        IRepository<Order> orderContext;
        public OrderService(IRepository<Order> _orderContext)
        {
            orderContext = _orderContext;

        }


        public void CreateOrder(Order _baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)
            {
                _baseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                                       
                });

            }
            orderContext.Insert(_baseOrder);
            orderContext.Commit();
        }

    }
}
