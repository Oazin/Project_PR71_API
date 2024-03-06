using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Models;

namespace Project_PR71_API.Services.IServices
{
    public interface IPostService
    {
        public ICollection<Post> GetPostsByUser(string userEmail);

        public bool AddPost(string userEmail, Post post);

        public bool DeletePost(int postId);

        public bool UpdatePost(int postId, Post post);

        public bool AddLikes(int postId, LikeViewModel newLike);
    }
}
