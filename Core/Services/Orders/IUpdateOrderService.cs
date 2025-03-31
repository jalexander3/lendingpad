using System.Collections.Generic;
using System;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order order, Guid userId, DateTime orderDate, Dictionary<Guid, int> products, decimal total);
    }
}
