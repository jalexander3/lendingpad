using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class OrderPricingService : IOrderPricingService
    {
        private readonly IProductRepository _productRepository;

        public OrderPricingService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public decimal CalculateTotal(Dictionary<Guid, int> products)
        {
            if (products == null || !products.Any())
                return 0;

            return products.Sum(p =>
            {
                var product = _productRepository.Get(p.Key);
                var quantity = p.Value;

                if (product == null || quantity <= 0)
                    return 0;

                return product.Price * quantity;
            });
        }
    }

}
