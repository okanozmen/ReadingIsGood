using System;

namespace ReadingIsGood.Domain.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? TokenEndDate { get; set; }
    }
}
