using System;
using System.Collections.Generic;

namespace ReadingIsGood.Domain.Models
{
    public class Order : BaseModel
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderProduct> Products { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
