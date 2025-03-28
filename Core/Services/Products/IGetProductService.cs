using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Products
{
    public interface IGetProductService
    {
        Product GetProduct(Guid id);

        IEnumerable<Product> GetProducts(string name = null, string sku = null, decimal? minPrice = null, decimal? maxPrice = null, IEnumerable<string> tags = null);
    }
}
