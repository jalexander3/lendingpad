using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderPricingService _orderPricingService;

        public CreateOrderService(
            IIdObjectFactory<Order> orderFactory,
            IOrderRepository orderRepository,
            IUpdateOrderService updateOrderService,
            IProductRepository productRepository,
            IOrderPricingService orderPricingService)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
            _updateOrderService = updateOrderService;
            _productRepository = productRepository;
            _orderPricingService = orderPricingService;
        }

        public Order Create(Guid id, Guid userId, DateTime orderDate, Dictionary<Guid, int> products)
        {
            if(products == null || !products.Any())
                throw new InvalidOperationException("Order must contain at least one product.");

            var existing = _orderRepository.Get(id);
            if (existing != null)
                throw new InvalidOperationException("Order already exists.");

            var missingProductIds = products
                .Where((productQuantities) => _productRepository.Get(productQuantities.Key) == null)
                .ToList();

            if (missingProductIds.Any())
                throw new InvalidOperationException($"One or more products were not found: {string.Join(", ", missingProductIds)}");

            var order = _orderFactory.Create(id);
            var price = _orderPricingService.CalculateTotal(products);
            _updateOrderService.Update(order, userId, orderDate, products, price);
            _orderRepository.Save(order);
            return order;
        }
    }
}
