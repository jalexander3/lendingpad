using BusinessEntities;
using Common;
using Data.Extensions;
using Data.Indexes;
using Raven.Client;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IDocumentSession _documentSession;

        public ProductRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Product> Get(string name = null, string sku = null, decimal? minPrice = null, decimal? maxPrice = null, IEnumerable<string> tags = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product, ProductsListIndex>();

            var hasFilter = false;

            if (!string.IsNullOrWhiteSpace(name))
                query = query.AndIfHasFilter(ref hasFilter).Where($"Name:*{name}*");

            if (!string.IsNullOrWhiteSpace(sku))
                query = query.AndIfHasFilter(ref hasFilter).WhereEquals("SKU", sku);

            if (minPrice.HasValue)
                query = query.AndIfHasFilter(ref hasFilter).WhereGreaterThanOrEqual("Price", minPrice.Value);

            if (maxPrice.HasValue)
                query = query.AndIfHasFilter(ref hasFilter).WhereLessThanOrEqual("Price", maxPrice.Value);

            if (tags != null && tags.Any())
                query = query.AndIfHasFilter(ref hasFilter).WhereIn("Tags", tags.ToArray());

            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<ProductsListIndex>();
        }
    }

}
