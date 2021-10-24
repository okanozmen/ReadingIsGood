using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace ReadingIsGood.Domain.MongoDb
{
    public interface IMongoDbContext
    {
        /// <typeparam name="T">The type of collected item</typeparam>
        /// <param name="projection">Includes or excludes fields in type T.</param>
        /// <param name="filter">Filter attribute.</param>
        /// <param name="collectionName">This value is optional. If, there are more than one collection, "collectionName" should
        /// be defined to define what collection is used by class.</param>
        List<T> Find<T>(Expression<Func<T, bool>> filter, Expression<Func<T, object>> projection = null, string collectionName = null, string dbName = null);
        List<T> FindAll<T>(Expression<Func<T, object>> projection = null, string collectionName = null);

        /// <param name="array">The array that matches on database items.</param>
        /// <param name="uniqueKey">The unique key of the type T. The array items are searched on defined unique key. It is generally "_id" parameter for mongodb.</param>
        List<T> FindAllInRange<T>(List<object> array, string uniqueKey, Expression<Func<T, object>> projection = null, string collectionName = null);
        /// <param name="item">The item type of T that will be inserted.</param>
        void InsertOne<T>(T item, string collectionName = null);
        Task InsertOneAsync<T>(T item, string collectionName = null);
        void InsertMany<T>(List<T> item, string collectionName = null);
        void UpdateOne<T, TValuefield>(Expression<Func<T, bool>> filter, Expression<Func<T, TValuefield>> newValueType, TValuefield newvalue, string collectionName = null);
        void UpdateValue<T>(Expression<Func<T, bool>> filter, T item, string collectionName = null, string dbName = null);
        void UpdateValueAsync<T>(Expression<Func<T, bool>> filter, T item, string collectionName = null);
        void DeleteOne<T>(Expression<Func<T, bool>> filter, string collectionName = null);
        /// <param name="propertyInfo">This value should be defined to detect the value is existed or not on database.</param>
        void DeleteMany<T>(PropertyInfo propertyInfo, List<T> items, string collectionName = null);
        Task<bool> AddOrUpdateAsync<T>(Expression<Func<T, bool>> filter, T item, string dbName = null, string colName = null);
        Task<bool> DeleteOneAsync<T>(Expression<Func<T, bool>> filter, string dbName = null, string colName = null);
        Task<List<R>> FindAllInRangeAsync<T, R>(List<object> array, string uniqueKey, Expression<Func<T, R>> projection = null, string dbName = null, string colName = null);
        Task<List<R>> FindAsync<T, R>(Expression<Func<T, bool>> filter, string colName = null);
        Task<long> DeleteManyAsync<T>(Expression<Func<T, bool>> filter, string dbName = null, string colName = null);
        Task InsertManyAsync<T>(List<T> docs, bool isOrdered = false, string dbName = null, string colName = null);
        List<T> SortByDescending<T>(Expression<Func<T, object>> projection = null, Expression<Func<T, object>> sortExpression = null, int? limit = null, string collectionName = null, string dbName = null);
        List<T> SortBy<T>(Expression<Func<T, object>> projection = null, Expression<Func<T, object>> sortExpression = null, int? limit = null, string collectionName = null, string dbName = null);

        /// <summary>
        /// This method upsert a value to database. Upsert mean is, the new value is wroten to database according to if the data is exist or not.
        /// If defined id is not included on database, the data is wrotten as a new value. Otherwise, the data is replaced with old.
        /// <exception>
        /// The expression and item differance causes the exception.
        /// For instance, if expression hits the id=1. But the item's id=2.
        /// So, expression and item unique id should hit the same value.
        /// </exception>
        /// </summary>
        void Upsert<T>(PropertyInfo propertyInfo, T item, string collectionName = null);
    }
}
