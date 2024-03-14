﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Controllers
{
    [Route("api/[controller]")]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService followService;

        public FollowController(IFollowService followService)
        {
            this.followService = followService;
        }

        [HttpGet("followings/{emailUser}")]
        public ICollection<FollowViewModel> GetFollowings([FromRoute] string emailUser)
        {
            return followService.GetFollowings(emailUser);
        }

        [HttpGet("follower/{emailUser}")]
        public ICollection<FollowViewModel> GetFollowers([FromRoute] string emailUser)
        {
            return followService.GetFollowers(emailUser);
        }

        [HttpPost]
        public bool AddFollow([FromBody] FollowViewModel follow)
        {
            return followService.AddFollow(follow);
        }

        [HttpDelete]
        public bool UnFollow([FromBody] FollowViewModel follow)
        {
            return followService.UnFollow(follow);
        }
    }
}
