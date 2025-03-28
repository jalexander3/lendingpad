using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;

namespace Core.Services.Products
{
    [AutoRegister]
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _productRepository;

        public GetProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product GetProduct(Guid id)
        {
            return _productRepository.Get(id);
        }

        public IEnumerable<Product> GetProducts(string name = null, string sku = null, decimal? minPrice = null, decimal? maxPrice = null, IEnumerable<string> tags = null)
        {
            return _productRepository.Get(name, sku, minPrice, maxPrice, tags);
        }
    }
}
