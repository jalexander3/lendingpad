using BusinessEntities;
using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class ProductsListIndex : AbstractIndexCreationTask<Product>
    {
        public ProductsListIndex()
        {
            Map = products => from product in products
                              select new
                              {
                                  product.Name,
                                  product.SKU,
                                  product.Price,
                                  product.Tags
                              };

            Index(x => x.Name, FieldIndexing.Analyzed);
            Index(x => x.SKU, FieldIndexing.NotAnalyzed);
            Index(x => x.Price, FieldIndexing.Default);
            Index(x => x.Tags, FieldIndexing.Default);
        }
    }

}
