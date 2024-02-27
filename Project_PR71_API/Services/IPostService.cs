using Project_PR71_API.Models;

namespace Project_PR71_API.Services
{
    public interface IPostService
    {
        public ICollection<Post> GetByUser(int userId);
    }
}
