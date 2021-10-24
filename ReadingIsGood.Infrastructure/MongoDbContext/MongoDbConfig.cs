using ReadingIsGood.Domain.IMongoDb;

namespace ReadingIsGood.Infrastructure.MongoDbContext
{
    public class MongoDbConfig : IMongoDbConfig
    {
        public string Address { get; set; }
        public string DbName { get; set; }
    }
}
