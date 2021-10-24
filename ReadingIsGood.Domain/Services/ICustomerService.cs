using System;
using System.Threading.Tasks;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.ResponseModels;

namespace ReadingIsGood.Domain.Services
{
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateCustomer(Customer customer);
        Task<CustomerResponse> GetCustomerInfo(Guid customerId);
    }
}
