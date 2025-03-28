using BusinessEntities;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Get(string name = null, string sku = null, decimal? minPrice = null, decimal? maxPrice = null, IEnumerable<string> tags = null);
        void DeleteAll();
    }

}
