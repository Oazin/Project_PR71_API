using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("feed", Name = "GetFeed")]
        public ICollection<PostViewModel> GetFeed()
        {
            return postService.GetFeed();
        }

        [HttpGet("{userEmail}", Name = "GetPostsByUser")]
        public ICollection<PostViewModel> GetPostsByUser([FromRoute] string userEmail)
        {
            return postService.GetPostsByUser(userEmail);
        }

        [HttpPost("{userEmail}", Name = "AddPost")]
        public bool AddPost([FromRoute] string userEmail, [FromBody] PostViewModel post)
        {
            return postService.AddPost(userEmail, post);
        }

        [HttpPatch("{idPost}", Name = "UpdatePost")]
        public bool UpdatePost([FromRoute] int idPost, [FromBody] Post post)
        {
            return postService.UpdatePost(idPost, post);
        }

        [HttpPatch("{idPost}/like", Name = "AddLikes")]
        public bool AddLike([FromRoute] int idPost, [FromBody] LikeViewModel newLikeViewModel)
        {
            return postService.AddLikes(idPost, newLikeViewModel);
        }

        [HttpDelete("{idPost}/{emailUser}")]
        public bool DeleteLike([FromRoute] int idPost, [FromRoute] string emailUser)
        {
            return postService.DeleteLike(idPost, emailUser);
        }

        [HttpGet("{idPost}/{emailUser}")]
        public bool HadLiked([FromRoute] int idPost, [FromRoute] string emailUser)
        {
            return postService.HadLiked(idPost, emailUser);
        }

        [HttpDelete("{idPost}", Name = "DeletePost")]
        public bool DeletePost([FromRoute] int idPost)
        {
            return postService.DeletePost(idPost);
        }
    }
}