using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Models;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Controllers
{

    [Route("api/[controller]")]
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
        public UserViewModel? GetUserByEmail(string email)
        {
            return userService.GetUserByEmail(email);
        }

        [HttpPost("tryconnection", Name = "ConnectUser")]
        public IActionResult TryToConnectUser(MailData mailData)
        {
            userService.ConnectUser(mailData.EmailAdress, mailData.EmailCode);
            return Ok("Success");
        }

        [HttpPatch("{email}", Name = "UpdateUser")]
        public bool UpdateUser(string email, UserViewModel user)
        {
            return userService.UpdateUser(email, user);
        }

    }
}
