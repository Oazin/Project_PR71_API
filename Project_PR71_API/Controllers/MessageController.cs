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

        [HttpGet("{idChat}/{emailCurrentUser}")]
        public ICollection<MessageViewModel> GetMessagesByConv([FromRoute] int idChat, [FromRoute] string emailCurrentUser)
        {
            return messageService.GetMessageByConv(idChat, emailCurrentUser);
        }

        [HttpPost]
        public bool SendMessage([FromBody] MessageViewModel messageViewModel)
        {
            return messageService.SendMessage(messageViewModel);
        }

        [HttpPatch("{idMessage}")]
        public async Task<bool> UpdateMessage([FromRoute] int idMessage, [FromBody] MessageViewModel messageViewModel)
        {
            return messageService.UpdateMessage(idMessage, messageViewModel);
        }

        [HttpDelete("{idMessage}")]
        public bool DeleteMessage([FromRoute] int idMessage)
        {
            return messageService.DeleteMessage(idMessage);
        }
    }
}
