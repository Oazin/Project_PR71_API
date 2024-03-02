using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Services.IServices
{
    public interface ICommentService
    {
        public bool AddComment(CommentViewModel commentViewModel);

        public ICollection<CommentViewModel> GetCommentsByPost(int idPost);

        public bool DeleteComment(int idComment);
    }
}
