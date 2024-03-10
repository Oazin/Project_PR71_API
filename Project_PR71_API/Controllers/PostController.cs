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

        [HttpGet("{userEmail}", Name = "GetPostsByUser")]
        public ICollection<PostViewModel> GetPostsByUser(string userEmail)
        {
            return postService.GetPostsByUser(userEmail);
        }

        [HttpPost("{userEmail}", Name = "AddPost")]
        public bool AddPost(string userEmail, PostViewModel post)
        {
            return postService.AddPost(userEmail, post);
        }

        [HttpPatch("{idPost}", Name = "UpdatePost")]
        public bool UpdatePost(int idPost, Post post)
        {
            return postService.UpdatePost(idPost, post);
        }

        [HttpPatch("{idPost}/like", Name = "AddLikes")]
        public bool AddLike(int idPost, LikeViewModel newLikeViewModel)
        {
            return postService.AddLikes(idPost, newLikeViewModel);
        }

        [HttpDelete("{idPost}/{emailUser}")]
        public bool DeleteLike(int idPost, string emailUser)
        {
            return postService.DeleteLike(idPost, emailUser);
        }

        [HttpGet("{idPost}/{emailUser}")]
        public bool HadLiked(int idPost, string emailUser)
        {
            return postService.HadLiked(idPost, emailUser);
        }

        [HttpDelete("{idPost}", Name = "DeletePost")]
        public bool DeletePost(int idPost)
        {
            return postService.DeletePost(idPost);
        }
    }
}