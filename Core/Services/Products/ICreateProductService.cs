using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    public interface ICreateProductService
    {
        Product Create(Guid id, string name, string sku, string description, decimal? price, int? stock, IEnumerable<string> tags);
    }
}
