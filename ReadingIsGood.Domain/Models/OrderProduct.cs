using System;

namespace ReadingIsGood.Domain.Models
{
    public class OrderProduct 
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
