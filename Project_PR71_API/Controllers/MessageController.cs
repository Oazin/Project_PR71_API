using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        
        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        public ICollection<MessageViewModel> GetMessagesByConv(string sender, string receiver)
        {
            return messageService.GetMessageByConv(sender, receiver);
        }

        [HttpPost]
        public bool SendMessage(MessageViewModel messageViewModel)
        {
            return messageService.SendMessage(messageViewModel);
        }

        [HttpPatch("{idMessage}")]
        public bool UpdateMessage(int idMessage, MessageViewModel messageViewModel)
        {
            return messageService.UpdateMessage(idMessage, messageViewModel);
        }

        [HttpDelete("{idMessage}")]
        public bool DeleteMessage(int idMessage)
        {
            return messageService.DeleteMessage(idMessage);
        }
    }
}
