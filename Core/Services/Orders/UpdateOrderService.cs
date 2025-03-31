using BusinessEntities;
using Common;
using System.Collections.Generic;
using System;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public void Update(Order order, Guid userId, DateTime orderDate, Dictionary<Guid, int> products, decimal totalPrice)
        {
            order.SetUserId(userId);
            order.SetOrderDate(orderDate);
            order.SetProducts(products);
            order.SetTotalPrice(totalPrice);
        }
    }
}
