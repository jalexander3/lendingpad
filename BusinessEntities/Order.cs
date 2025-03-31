using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private Guid _userId;
        private DateTime _orderDate;
        private Dictionary<Guid, int> _products;
        private decimal _totalPrice;

        public Guid UserId => _userId;
        public DateTime OrderDate => _orderDate;
        public Dictionary<Guid, int> Products => _products;
        public decimal TotalPrice => _totalPrice;

        public void SetUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId must be a valid GUID.", nameof(userId));

            _userId = userId;
        }

        public void SetOrderDate(DateTime orderDate)
        {
            if (orderDate == default)
                throw new ArgumentException("OrderDate must be set.", nameof(orderDate));

            _orderDate = orderDate;
        }

        public void SetProducts(Dictionary<Guid, int> products)
        {
            if (products == null || products.Count == 0)
                throw new ArgumentException("Products must contain at least one item.", nameof(products));

            if (products.Any(p => p.Key == Guid.Empty))
                throw new ArgumentException("All product keys must be valid GUIDs.", nameof(products));

            if (products.Any(p => p.Value <= 0))
                throw new ArgumentException("All product quantities must be greater than zero.", nameof(products));

            _products = new Dictionary<Guid, int>(products);
        }

        public void SetTotalPrice(decimal totalPrice)
        {
            if (totalPrice < 0)
                throw new ArgumentException("TotalPrice cannot be negative.", nameof(totalPrice));

            _totalPrice = totalPrice;
        }
    }
}
