using Project_PR71_API.Models;

namespace Project_PR71_API.Services
{
    public interface IUserService
    {
        bool AddUser(User user);
        public ICollection<Post> GetPosts(string userEmail);
    }
}
