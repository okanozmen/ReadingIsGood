using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ReadingIsGood.API.Resources;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.ResponseModels;
using ReadingIsGood.Domain.Services;

namespace ReadingIsGood.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates order.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderResource orderResource)
        {
            Order order = mapper.Map<OrderResource, Order>(orderResource);

            OrderResponse orderResponse = await orderService.CreateOrder(order);

            return orderResponse.IsSuccess ? Ok(orderResponse.order) : BadRequest(orderResponse.Message);
        }

        /// <summary>
        /// Gets orders of the customer.
        /// </summary>
        [HttpGet("{customerId:Guid}")]
        public async Task<IActionResult> GetOrder(Guid customerId)
        {
            CustomerOrderListResponse customerOrder = await orderService.GetOrder(customerId);

            return customerOrder.IsSuccess ? Ok(customerOrder.orderList) : BadRequest(customerOrder.Message);
        }

        /// <summary>
        /// Update order status, 0 = Processing, 1 = Delivered, 2 = Cancelled.
        /// </summary>
        [HttpPost("UpdateOrder")]
        public IActionResult UpdateOrder(Guid orderId, OrderStatus status)
        {
            var orderResponse = orderService.UpdateOrderStatus(orderId, status);

            if (orderResponse.IsSuccess)
            {
                orderResponse.Message = "Order status has updated successfully.";
            }

            return orderResponse.IsSuccess ? Ok(orderResponse.Message) : BadRequest(orderResponse.Message);
        }

        /// <summary>
        /// Gets order details.
        /// </summary>
        [HttpGet("GetOrderDetails")]
        public async Task<IActionResult> GetOrderDetail(Guid orderId)
        {
            var orderResponse = await orderService.GetOrderDetail(orderId);

            return orderResponse.IsSuccess ? Ok(orderResponse.order) : BadRequest(orderResponse.Message);
        }
    }
}
