using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models;
using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Services.IServices
{
    public interface IUserService
    {
        public void ConnectUser(string email, string code);

        public UserViewModel? GetUserByEmail(string email);

        public ICollection<UserViewModel>? ResearchUsers(string searchTerms);

        public bool UpdateUser(string email, UserViewModel user);
    }
}
