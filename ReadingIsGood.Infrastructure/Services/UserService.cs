using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Domain.ResponseModels;
using ReadingIsGood.Domain.Services;
using System;

namespace ReadingIsGood.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserResponse AddUser(User user)
        {
            try
            {
                userRepository.AddUser(user);
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse(ex.Message);
            }
        }

        public void SaveToken(string userId, string token)
        {
            try
            {
                userRepository.SaveToken(userId, token);
            }
            catch (Exception)
            {
                //Logging
            }
        }
        public UserResponse CheckUserLoginInfo(string mail, string password)
        {
            try
            {
                User user = userRepository.CheckUserLoginInfo(mail, password);

                return user == null ? new UserResponse(false, "Invalid username or password", null) : new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse(ex.Message);
            }
        }

        public UserResponse FindByEmail(string email)
        {
            try
            {
                User user = userRepository.FindByEmail(email);

                return user == null ? new UserResponse(false, "The user can not be found", null) : new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse(ex.Message);
            }
        }
    }
}
