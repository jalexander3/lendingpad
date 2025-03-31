using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetMany(Guid? userId = null, DateTime? fromDate = null, DateTime? toDate = null, IEnumerable<Guid> products = null);
        void DeleteAll();
    }
}
