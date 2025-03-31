using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderPricingService _orderPricingService;

        public GetOrderService(IOrderRepository orderRepository, IOrderPricingService orderPricingService)
        {
            _orderRepository = orderRepository;
            _orderPricingService = orderPricingService;
        }

        public Order GetOrder(Guid id)
        {
            var order = _orderRepository.Get(id);

            if (order is null) return order;

            order.SetTotalPrice(_orderPricingService.CalculateTotal(order.Products));
            return order;
        }

        public IEnumerable<Order> GetOrders(Guid? userId = null, DateTime? fromDate = null, DateTime? toDate = null, IEnumerable<Guid> products = null)
        {
            var orders = _orderRepository.GetMany(userId, fromDate, toDate, products);
            foreach(Order order in orders)
            {
                order.SetTotalPrice(_orderPricingService.CalculateTotal(order.Products));
            }
            return orders;
        }
    }
}
