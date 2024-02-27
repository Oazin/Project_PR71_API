using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models;
using Project_PR71_API.Services;

namespace Project_PR71_API.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly ILogger<PostController> _logger;
        private readonly IPostService postService;

        public PostController(ILogger<PostController> logger, IPostService postService)
        {
            _logger = logger;
            this.postService = postService;
        }

        [HttpGet("{userId}", Name = "GetPostByUser")]
        public ICollection<Post> GetByUser(int userId)
        {
            return postService.GetByUser(userId);
        }
    }
}