﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Models;
using Project_PR71_API.Services.IServices;
using MailKit.Search;

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
        public UserViewModel? GetUserByEmail([FromRoute] string email)
        {
            return userService.GetUserByEmail(email);
        }

        [HttpPost("tryconnection", Name = "ConnectUser")]
        public bool TryToConnectUser([FromBody] MailData mailData)
        {
            userService.ConnectUser(mailData.EmailAdress, mailData.Code);
            return true;
        }

        [HttpPatch("{email}", Name = "UpdateUser")]
        public bool UpdateUser([FromRoute] string email, [FromBody] UserViewModel user)
        {
            return userService.UpdateUser(email, user);
        }

        [HttpGet("research/{searchTerms}", Name = "ResearchUser")]
        public ICollection<UserViewModel>? ResearchUser([FromRoute] string searchTerms)
        {
            return userService.ResearchUsers(searchTerms);
        }
    }
}
