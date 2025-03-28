using BusinessEntities;
using Common;
using System.Collections.Generic;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, string sku, string description, decimal? price, int? stock, IEnumerable<string> tags)
        {
            product.SetName(name);
            product.SetSKU(sku);
            product.SetDescription(description);
            product.SetPrice(price);
            product.SetStock(stock);
            product.SetTags(tags);
        }
    }
}
