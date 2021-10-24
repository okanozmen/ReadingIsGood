using System;
using System.Collections.Generic;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.Domain.ResponseModels
{
    public class CustomerOrderListResponse : BaseResponse
    {
        public IEnumerable<Order> orderList { get; set; }
        public CustomerOrderListResponse(bool isSuccess, string message, IEnumerable<Order> orderList) : base(isSuccess, message)
        {
            this.orderList = orderList;
        }
        public CustomerOrderListResponse(IEnumerable<Order> orderList) : this(true, String.Empty, orderList) { }
        public CustomerOrderListResponse(string message) : this(false, message, null) { }
    }
}
