using BusinessEntities;
using System.Collections.Generic;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        void Update(Product product, string name, string sku, string description, decimal? price, int? stock, IEnumerable<string> tags);
    }
}
