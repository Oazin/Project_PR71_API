using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models;

namespace Project_PR71_API.Services
{
    public interface IUserService
    {
        public void ConnectUser(string email, string code);
        
        public User? GetUserByEmail( string email);
    }
}
