using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Models;

namespace Project_PR71_API.Services.IServices
{
    public interface IPostService
    {
        public ICollection<PostViewModel> GetPostsByUser(string userEmail);

        public bool AddPost(string userEmail, PostViewModel postViewModel);

        public bool DeletePost(int idPost);

        public bool UpdatePost(int idPost, Post post);

        public bool AddLikes(int idPost, LikeViewModel newLike);

        public bool HadLiked(int idPost, string emailUser);

        public bool DeleteLike(int idPost, string emailUser);
    }
}
