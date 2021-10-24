using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Domain.ResponseModels;
using ReadingIsGood.Domain.Services;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<CustomerResponse> CreateCustomer(Customer customer)
        {
            try
            {
                var customerDetail = await customerRepository.CheckCustomerExistAsync(customer.Email, customer.Phone);

                if (customerDetail != null)
                {
                    return new CustomerResponse("Customer already exists");
                }

                Guid customerId = Guid.NewGuid();
                customer.CustomerId = customerId;
                customerRepository.CreateCustomer(customer);

                return new CustomerResponse(customer);
            }
            catch (Exception ex)
            {
                return new CustomerResponse(ex.Message);
            }
        }

        public async Task<CustomerResponse> GetCustomerInfo(Guid customerId)
        {
            try
            {
                var customerInfo = await customerRepository.GetCustomerInfoAsync(customerId);

                if (customerInfo == null)
                {
                    return new CustomerResponse("Customer not found");
                }

                return new CustomerResponse(customerInfo);
            }
            catch (Exception ex)
            {
                return new CustomerResponse(ex.Message);
            }
        }
    }
}
