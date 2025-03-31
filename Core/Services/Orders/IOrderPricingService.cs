using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Orders
{
    public interface IOrderPricingService
    {
        decimal CalculateTotal(Dictionary<Guid, int> products);
    }

}
