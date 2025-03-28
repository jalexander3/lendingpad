using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private string _name;
        private string _sku;
        private string _description;
        private decimal _price;
        private int _stock;
        private readonly List<string> _tags = new List<string>();

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string SKU
        {
            get => _sku;
            private set => _sku = value;
        }

        public string Description
        {
            get => _description;
            private set => _description = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public int Stock
        {
            get => _stock;
            private set => _stock = value;
        }

        public IEnumerable<string> Tags => _tags;

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Product name is required.");

            _name = name;
        }

        public void SetSKU(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentNullException(nameof(sku), "SKU is required.");

            _sku = sku;
        }

        public void SetDescription(string description)
        {
            _description = description ?? string.Empty;
        }

        public void SetPrice(decimal? price)
        {
            if (!price.HasValue || price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be a non-negative number.");

            _price = price.Value;
        }

        public void SetStock(int? stock)
        {
            if (!stock.HasValue || stock < 0)
                throw new ArgumentOutOfRangeException(nameof(stock), "Stock must be a non-negative number.");

            _stock = stock.Value;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }
}
