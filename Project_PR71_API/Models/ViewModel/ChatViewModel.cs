using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;

namespace Project_PR71_API.Models.ViewModel
{
    public class ChatViewModel
    {
        public int Id { get; set; }

        public UserViewModel User1 { get; set; }

        public UserViewModel User2 { get; set; }

        public ICollection<MessageViewModel>? messages { get; set; }

        public Chat Convert()
        {
            return new Chat
            {
                Id = Id,
            };
        }
    }
}
