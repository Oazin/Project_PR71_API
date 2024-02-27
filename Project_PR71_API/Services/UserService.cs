using Project_PR71_API.Configuration;
using Project_PR71_API.Models;

namespace Project_PR71_API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext dataContext;

        public UserService(DataContext dataContext) 
        { 
            this.dataContext = dataContext;
        }

        public ICollection<Post> GetPosts(string userEmail)
        {
            return dataContext.Post.Where(x => x.User.Email == userEmail).OrderBy(x => x.DateTime).ToList();
        }
    }
}
