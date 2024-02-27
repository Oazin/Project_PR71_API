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

        public bool AddUser(User user)
        {
            return true;
        }
    }
}
