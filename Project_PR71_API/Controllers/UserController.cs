using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Services;
using Project_PR71_API.Models;

namespace Project_PR71_API.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }

        [HttpGet("{email}", Name = "GetUserByEmail")]
        public ICollection<User> GetUserByEmail(string email)
        {
            return null;//userService.GetUsers();
        }

        [HttpPost(Name = "AddUser")]
        public bool AddUser(User user)
        {
            return userService.AddUser(user);
        }

        //[HttpPatch("{User}")]
    }
}
