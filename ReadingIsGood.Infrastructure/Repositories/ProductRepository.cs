using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.MongoDb;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Infrastructure.MongoDbContext;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReadingIsGood.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDbContext mongoDbContext;
        public ProductRepository(IMongoDbContext mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
        }
        public async Task<Product> GetProductDetailByIdAsync(Guid productId)
        {
            var productDetail = await mongoDbContext.FindAsync<Product, Product>(x => x.ProductId == productId, CollectionName.Product.ToString());
            return productDetail.FirstOrDefault();
        }

        public void UpdateProductStock(Guid productId, int stock)
        {
            Expression<Func<Product, bool>> filter = x => x.ProductId == productId;
            mongoDbContext.UpdateOne(filter, x => x.Stock, stock, CollectionName.Product.ToString());
        }
    }
}
