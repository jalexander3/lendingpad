using BusinessEntities;
using System.Collections.Generic;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            SKU = product.SKU;
            Description = product.Description;
            Price = product.Price;
            Stock = product.Stock;
            Tags = product.Tags;
        }

        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}