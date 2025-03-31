using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly Dictionary<Guid, Order> _orders = new Dictionary<Guid, Order>();

        public Order Get(Guid id)
        {
            _orders.TryGetValue(id, out var order);
            return order;
        }

        public IEnumerable<Order> GetMany(Guid? userId = null, DateTime? fromDate = null, DateTime? toDate = null, IEnumerable<Guid> products = null)
        {
            var query = _orders.Values.AsQueryable();

            if (userId != null)
                query = query.Where(o => o.UserId == userId);

            if (fromDate.HasValue)  
                query = query.Where(o => o.OrderDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(o => o.OrderDate <= toDate.Value);

            if (products != null && products.Any())
                query = query.Where(o => o.Products.Keys.Any(id => products.Contains(id)));

            return query.ToList();
        }

        public void Save(Order order)
        {
            _orders[order.Id] = order;
        }

        public void Delete(Order order)
        {
            _orders.Remove(order.Id);
        }

        public void DeleteAll()
        {
            _orders.Clear();
        }
    }
}
