using System;
using System.Collections.Generic;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.API.Resources
{
    public class OrderResource
    {
        public Guid CustomerId { get; set; }
        public List<OrderProduct> Products { get; set; }
    }
}
