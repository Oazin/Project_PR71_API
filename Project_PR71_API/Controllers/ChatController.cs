using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase 
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService) 
        {
            this.chatService = chatService;
        }

        [HttpPost]
        public bool CreateChat([FromBody] ChatViewModel chatViewModel)
        {
            return chatService.CreateChat(chatViewModel);
        }

        [HttpGet("{email}")]
        public ICollection<ChatViewModel> GetChatByEmail([FromRoute] string email)
        {
            return chatService.GetChatByEmail(email);
        }

        [HttpDelete("{idChat}")]
        public bool DeleteChat([FromRoute] int idChat) 
        {
            return chatService.DeleteChat(idChat);
        }

        [HttpGet("notif/{emailCurrentUser}")]    
        public ICollection<int> GetNotification([FromRoute] string emailCurrentUser)
        {
            return chatService.GetNotification(emailCurrentUser);
        }
    }
}
