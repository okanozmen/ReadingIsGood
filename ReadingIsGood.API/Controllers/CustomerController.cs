using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using ReadingIsGood.API.Resources;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.ResponseModels;
using ReadingIsGood.Domain.Services;

namespace ReadingIsGood.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates customer.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerResource customerResource)
        {
            Customer customer = mapper.Map<CustomerResource, Customer>(customerResource);

            CustomerResponse customerResponse = await customerService.CreateCustomer(customer);

            return customerResponse.IsSuccess ? Ok(customerResponse.customer) : BadRequest(customerResponse.Message);
        }


        /// <summary>
        /// Gets customer information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCustomerInfo(Guid customerId)
        {
            CustomerResponse customerResponse = await customerService.GetCustomerInfo(customerId);

            return customerResponse.IsSuccess ? Ok(customerResponse.customer) : BadRequest(customerResponse.Message);
        }
    }
}
