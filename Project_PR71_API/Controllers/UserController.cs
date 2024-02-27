using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Services;

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
    }
}
