namespace ReadingIsGood.Domain.IMongoDb
{
    public interface IMongoDbConfig
    {
        public string Address { get; set; }
        public string DbName { get; set; }
    }
}
