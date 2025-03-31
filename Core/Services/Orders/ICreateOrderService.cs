using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Order Create(Guid id, Guid userId, DateTime orderDate, Dictionary<Guid, int> products);
    }
}
