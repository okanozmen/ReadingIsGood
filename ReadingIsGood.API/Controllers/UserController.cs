using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ReadingIsGood.API.Resources;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.ResponseModels;
using ReadingIsGood.Domain.Services;

namespace ReadingIsGood.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets user information by email.
        /// </summary>
        [HttpGet]
        public IActionResult GetUser(string email)
        {
            IEnumerable<Claim> claims = User.Claims;

            UserResponse userResponse = userService.FindByEmail(email);

            return userResponse.IsSuccess ? Ok(userResponse.user) : BadRequest(userResponse.Message);
        }

        /// <summary>
        /// Adds user.
        /// </summary>
        [HttpPost]
        public IActionResult AddUser(UserResource userResource)
        {
            User user = mapper.Map<UserResource, User>(userResource);

            UserResponse userResponse = userService.AddUser(user);

            return userResponse.IsSuccess ? Ok(userResponse.user) : BadRequest(userResponse.Message);
        }
    }
}
