using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models;
using Project_PR71_API.Services.IServices;

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

        [HttpGet("{userEmail}", Name = "GetPostsByUser")]
        public ICollection<Post> GetPostsByUser(string userEmail)
        {
            return postService.GetPostsByUser(userEmail);
        }

        [HttpPost("{userEmail}", Name = "AddPost")]
        public bool AddPost(string userEmail, Post post)
        {
            return postService.AddPost(userEmail, post);
        }

        [HttpPatch("{postId}", Name = "UpdatePost")]
        public bool UpdatePost(int postId, Post post)
        {
            return postService.UpdatePost(postId, post);
        }

        [HttpPatch("{postId}/{likes}", Name = "AddLikes")]
        public bool AddLikes(int postId, int likes)
        {
            return postService.AddLikes(postId, likes);
        }

        [HttpDelete("{postId}", Name = "DeletePost")]
        public bool DeletePost(int postId)
        {
            return postService.DeletePost(postId);
        }
    }
}