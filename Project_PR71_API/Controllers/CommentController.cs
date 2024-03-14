using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost()]
        public bool AddComment([FromBody] CommentViewModel comment)
        {
            return commentService.AddComment(comment);
        }

        [HttpGet("{idPost}")]
        public ICollection<CommentViewModel> GetCommentsByPost(int idPost)
        {
            return commentService.GetCommentsByPost(idPost);
        }

        [HttpDelete("{idPost}")]
        public bool DeleteComment(int idPost)
        {
            return commentService.DeleteComment(idPost);
        }
    }
}
