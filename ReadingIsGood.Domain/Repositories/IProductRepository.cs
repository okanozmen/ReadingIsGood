using System;
using System.Threading.Tasks;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductDetailByIdAsync(Guid productId);
        void UpdateProductStock(Guid productId, int stock);
    }
}
