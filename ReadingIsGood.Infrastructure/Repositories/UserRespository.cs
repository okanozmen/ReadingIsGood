using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.MongoDb;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Infrastructure.MongoDbContext;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ReadingIsGood.Infrastructure.Repositories
{
    public class UserRespository : IUserRepository
    {
        private readonly IMongoDbContext mongoDbContext;
        public UserRespository(IMongoDbContext mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
        }

        public void AddUser(User user)
        {
            mongoDbContext.InsertOne(user, CollectionName.User.ToString());
        }

        public void SaveToken(string userId, string token)
        {
            Expression<Func<User, bool>> filter = x => x.Id == userId;
            mongoDbContext.UpdateOne(filter, x => x.Token, token, CollectionName.User.ToString());
        }
        public User CheckUserLoginInfo(string email, string password)
        {
            var user = mongoDbContext.Find<User>(x => x.Email == email && x.Password == password, null, CollectionName.User.ToString());
            return user.FirstOrDefault();
        }

        public User FindByEmail(string email)
        {
            var user = mongoDbContext.Find<User>(x => x.Email == email, null, CollectionName.User.ToString());
            return user.FirstOrDefault();
        }
    }
}
