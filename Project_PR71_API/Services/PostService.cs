using Project_PR71_API.Models;

namespace Project_PR71_API.Services
{

    public class PostService : IPostService
    {
        private readonly DataContext dataContext;

        public PostService(DataContext dataContext) 
        {
            this.dataContext = dataContext;
        }

        public ICollection<Post> GetByUser(int userId)
        {
            return dataContext.Post.Where(x => x.User.Id == userId).ToList();
        }
    }
}
