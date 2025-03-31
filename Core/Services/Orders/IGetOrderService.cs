using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        Order GetOrder(Guid id);
        IEnumerable<Order> GetOrders(Guid? userId = null, DateTime? fromDate = null, DateTime? toDate = null, IEnumerable<Guid> products = null);
    }
}
