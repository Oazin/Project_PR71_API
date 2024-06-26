﻿using Project_PR71_API.Models;
using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Services.IServices
{
    public interface IMessageService
    {
        public bool SendMessage(MessageViewModel messageViewModel);

        public ICollection<MessageViewModel> GetMessageByConv(int IdChat, string emailCurrentUser);

        public bool DeleteMessage(int idMessage);

        public bool UpdateMessage(int idMessage, MessageViewModel messageViewModel);

        public bool HadReaded(int idChat, string emailSender);
    }
}
