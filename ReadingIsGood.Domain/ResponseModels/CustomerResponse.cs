using System;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.Domain.ResponseModels
{
    public class CustomerResponse : BaseResponse
    {
        public Customer customer { get; set; }
        public CustomerResponse(bool isSuccess, string message, Customer customer) : base(isSuccess, message)
        {
            this.customer = customer;
        }
        public CustomerResponse(Customer customer) : this(true, String.Empty, customer) { }
        public CustomerResponse(string message) : this(false, message, null) { }
    }
}
