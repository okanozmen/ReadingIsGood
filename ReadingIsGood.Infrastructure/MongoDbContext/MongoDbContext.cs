using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReadingIsGood.Domain.IMongoDb;
using ReadingIsGood.Domain.MongoDb;


namespace ReadingIsGood.Infrastructure.MongoDbContext
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;

        public MongoDbContext(IMongoDbConfig mongoDbconfig)
        {
            client = new MongoClient(mongoDbconfig.Address);
            database = client.GetDatabase(mongoDbconfig.DbName);
        }

        IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return database.GetCollection<T>(collectionName);
        }

        public void DeleteOne<T>(Expression<Func<T, bool>> expression, string collectionName = null)
        {
            GetCollection<T>(collectionName).DeleteOne(expression);
        }
        public void DeleteMany<T>(PropertyInfo propertyInfo, List<T> items, string collectionName = null)
        {
            var collection = GetCollection<T>(collectionName);
            FilterDefinition<T> filter = null;
            foreach (var item in items)
            {
                if (filter == null)
                    filter = Builders<T>.Filter.Eq(propertyInfo.Name, propertyInfo.GetValue(item));
                else
                    filter = filter | Builders<T>.Filter.Eq(propertyInfo.Name, propertyInfo.GetValue(item));
            }

            collection.DeleteMany(filter);
        }
        public List<T> Find<T>(Expression<Func<T, bool>> filter, Expression<Func<T, object>> projection = null, string collectionName = null, string dbName = null)
        {
            var findFilter = GetCollection<T>(collectionName).Find(filter);
            if (projection != null)
                findFilter.Project(projection);
            return findFilter.ToList();
        }
        public List<T> SortByDescending<T>(Expression<Func<T, object>> projection = null, Expression<Func<T, object>> sortExpression = null, int? limit = null, string collectionName = null, string dbName = null)
        {
            var findFilter = GetCollection<T>(collectionName).Find(Builders<T>.Filter.Empty).SortByDescending(sortExpression).Limit(limit);
            if (projection != null)
                findFilter.Project(projection);
            return findFilter.ToList();
        }
        public List<T> SortBy<T>(Expression<Func<T, object>> projection = null, Expression<Func<T, object>> sortExpression = null, int? limit = null, string collectionName = null, string dbName = null)
        {
            var findFilter = GetCollection<T>(collectionName).Find(Builders<T>.Filter.Empty).SortBy(sortExpression).Limit(limit);
            if (projection != null)
                findFilter.Project(projection);
            return findFilter.ToList();
        }
        public List<T> FindAll<T>(Expression<Func<T, object>> projection = null, string collectionName = null)
        {
            var findFilter = GetCollection<T>(collectionName).Find(Builders<T>.Filter.Empty);
            if (projection != null)
                findFilter.Project(projection);
            return findFilter.ToList();
        }
        public List<T> FindAllInRange<T>(List<object> array, string uniqueKey, Expression<Func<T, object>> projection = null, string collectionName = null)
        {
            var findFilter = GetCollection<T>(collectionName).Find(Builders<T>.Filter.AnyIn(uniqueKey, array));
            if (projection != null)
                findFilter.Project(projection);
            return findFilter.ToList();
        }
        public void InsertMany<T>(List<T> item, string collectionName = null)
        {
            GetCollection<T>(collectionName).InsertMany(item);
        }
        public void InsertOne<T>(T item, string collectionName = null)
        {
            GetCollection<T>(collectionName).InsertOne(item);
        }
        public Task InsertOneAsync<T>(T item, string collectionName = null)
        {
            return GetCollection<T>(collectionName).InsertOneAsync(item);
        }
        public void UpdateOne<T, TValuefield>(Expression<Func<T, bool>> expression,
            Expression<Func<T, TValuefield>> valueField, TValuefield item, string collectionName = null)
        {
            GetCollection<T>(collectionName).UpdateMany(expression, Builders<T>.Update.Set(valueField, item));
        }
        public void UpdateValueAsync<T>(Expression<Func<T, bool>> expression, T item, string collectionName = null)
        {
            GetCollection<T>(collectionName).ReplaceOneAsync(expression, item);
        }
        public void UpdateValue<T>(Expression<Func<T, bool>> expression, T item, string collectionName = null, string dbName = null)
        {
            GetCollection<T>(collectionName).ReplaceOne(expression, item);
        }
        public void Upsert<T>(PropertyInfo propertyInfo, T item, string collectionName = null)
        {
            var filter = Builders<T>.Filter.Eq(propertyInfo.Name, propertyInfo.GetValue(item));
            GetCollection<T>(collectionName).ReplaceOne(filter, item, new UpdateOptions { IsUpsert = true });
        }
        public async Task<bool> AddOrUpdateAsync<T>(Expression<Func<T, bool>> filter, T item, string dbName = null, string colName = null)
        {
            return (await GetCollection<T>(colName).ReplaceOneAsync(filter, item, new UpdateOptions { IsUpsert = true })).IsAcknowledged;
        }
        public async Task<bool> DeleteOneAsync<T>(Expression<Func<T, bool>> expression, string dbName = null, string colName = null)
        {
            return (await GetCollection<T>(colName).DeleteOneAsync(expression)).IsAcknowledged;
        }
        public async Task<List<R>> FindAllInRangeAsync<T, R>(List<object> array, string uniqueKey, Expression<Func<T, R>> projection = null, string dbName = null, string colName = null)
        {
            FindOptions<T, R> options = null;
            if (projection != null)
                options = new FindOptions<T, R>
                {
                    Projection = Builders<T>.Projection.Expression(projection)
                };
            return await (await GetCollection<T>(colName).FindAsync(Builders<T>.Filter.AnyIn(uniqueKey, array), options)).ToListAsync();
        }
        public async Task<List<R>> FindAsync<T, R>(Expression<Func<T, bool>> filter, string colName = null)
        {
            FindOptions<T, R> options = null;

            return await (await GetCollection<T>(colName).FindAsync(filter, options)).ToListAsync();
        }
        public async Task<long> DeleteManyAsync<T>(Expression<Func<T, bool>> filter, string dbName = null, string colName = null)
        {
            var result = await GetCollection<T>(colName).DeleteManyAsync(filter);
            return result.DeletedCount;
        }
        public Task InsertManyAsync<T>(List<T> docs, bool isOrdered = false, string dbName = null, string colName = null)
        {
            return GetCollection<T>(colName).InsertManyAsync(docs, new InsertManyOptions { IsOrdered = isOrdered });
        }
    }
}
