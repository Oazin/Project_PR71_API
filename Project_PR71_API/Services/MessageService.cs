using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Configuration;
using Project_PR71_API.Models;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Services
{
    public class MessageService : IMessageService
    {
        private readonly DataContext dataContext;

        public MessageService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Send a message
        /// </summary>
        /// <param name="messageViewModel"></param>
        /// <returns> boolean </returns>
        public bool SendMessage(MessageViewModel messageViewModel)
        {
            if (messageViewModel == null) { return false; }
            messageViewModel.Id = dataContext.Message.Any() ? dataContext.Message.Max(x => x.Id) + 1 : 1;

            Message? message = messageViewModel.Convert();
            message.Chat = dataContext.Chat.FirstOrDefault(x => x.Id == messageViewModel.IdChat);
            message.Sender = dataContext.User.FirstOrDefault(x => x.Email == messageViewModel.emailSender);
            if (message.Sender == null || message.Chat == null) { return false; }

            dataContext.Message.Add(message);

            dataContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// Get all messages of a conversation
        /// </summary>
        /// <param name="idChat"></param>
        /// <returns> ICollections of messages view model </returns>
        public ICollection<MessageViewModel> GetMessageByConv(int idChat)
        {
            List<Message> messages = dataContext.Message.Include(x => x.Sender).Include(x => x.Chat).Where(x => x.Chat.Id == idChat).OrderByDescending(x => x.Id).ToList();
            List<MessageViewModel> messageViewModels = messages.Select(x => x.Convert()).ToList();

            return messageViewModels;
        }

        /// <summary>
        /// Delete a message
        /// </summary>
        /// <param name="idMessage"></param>
        /// <returns> boolean </returns>
        public bool DeleteMessage(int idMessage)
        {
            Message? msg = dataContext.Message.FirstOrDefault(x => x.Id == idMessage);

            if (msg == null) { return false; }

            dataContext.Message.Remove(msg);

            dataContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Update a message
        /// </summary>
        /// <param name="idMessage"></param>
        /// <param name="message"></param>
        /// <returns> boolean </returns>
        public bool UpdateMessage(int idMessage, MessageViewModel message)
        {
            Message? existingMessage = dataContext.Message.Include(x => x.Sender).Include(x => x.Chat).FirstOrDefault(x => x.Id == idMessage);

            bool patched = false;

            if (existingMessage == null) { return patched; }

            if (existingMessage.Sender != null && existingMessage.Chat != null)
            {
                if (!existingMessage.Content.Equals(message.Content) || string.IsNullOrEmpty(message.Content))
                {
                    existingMessage.Content = message.Content;
                    patched = true;
                }
            }
            dataContext.SaveChangesAsync();

            return patched;
        }
    }
}
