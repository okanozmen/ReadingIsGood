using System;

namespace ReadingIsGood.Domain.Models
{
    public class Product : BaseModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
