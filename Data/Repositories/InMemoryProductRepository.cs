using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly Dictionary<Guid, Product> _products = new Dictionary<Guid, Product>();

        public Product Get(Guid id)
        {
            _products.TryGetValue(id, out var product);
            return product;
        }

        public IEnumerable<Product> Get(string name = null, string sku = null, decimal? minPrice = null, decimal? maxPrice = null, IEnumerable<string> tags = null)
        {
            var query = _products.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(sku))
                query = query.Where(p => string.Equals(p.SKU, sku, StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (tags != null && tags.Any())
                query = query.Where(p => p.Tags.Any(t => tags.Contains(t)));

            return query.ToList();
        }

        public void Save(Product product)
        {
            _products[product.Id] = product;
        }

        public void Delete(Product product)
        {
            _products.Remove(product.Id);
        }

        public void DeleteAll()
        {
            _products.Clear();
        }
    }

}
