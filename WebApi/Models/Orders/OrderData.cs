using BusinessEntities;
using System;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            UserId = order.UserId;
            OrderDate = order.OrderDate;
            Products = order.Products;
            Total = order.TotalPrice;
        }

        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public Dictionary<Guid, int> Products { get; set; }
    }
}