using System;
using System.Threading.Tasks;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.Domain.Repositories
{
    public interface ICustomerRepository
    {
        void CreateCustomer(Customer customer);
        Task<Customer> GetCustomerInfoAsync(Guid customerId);
        Task<Customer> CheckCustomerExistAsync(string email, string phone);
    }
}
