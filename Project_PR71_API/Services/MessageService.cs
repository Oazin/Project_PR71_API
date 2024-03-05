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

        public bool SendMessage(MessageViewModel messageViewModel)
        {
            if (messageViewModel == null) { return false; }

            Message? message = messageViewModel.Convert();
            message.Receiver = dataContext.User.FirstOrDefault(x => x.Email == messageViewModel.emailReceiver);
            message.Sender = dataContext.User.FirstOrDefault(x => x.Email == messageViewModel.emailSender);
            if (message.Sender == null || message.Receiver == null) { return false; }

            dataContext.Message.AddAsync(message);

            dataContext.SaveChangesAsync();

            return true;
        }

        public ICollection<MessageViewModel> GetMessageByConv(string sender, string receiver)
        {
            List<Message> messages = dataContext.Message.Where(x => (x.Sender.Email == sender && x.Receiver.Email == receiver) || (x.Sender.Email == receiver && x.Receiver.Email == sender)).OrderByDescending(x => x.Id).ToList();
            List<MessageViewModel> messageViewModels = new List<MessageViewModel>();


            foreach (Message message in messages)
            {
                message.Sender = dataContext.User.FirstOrDefault(x => x.Email == sender);
                message.Receiver = dataContext.User.FirstOrDefault(x => x.Email == receiver);
                if (message.Sender != null && message.Receiver != null) 
                {
                    messageViewModels.Add(message.Convert());
                }
            }

            return messageViewModels;
        }

        public bool DeleteMessage(int idMessage)
        {
            Message? msg = dataContext.Message.FirstOrDefault(x => x.Id == idMessage);

            if (msg == null) { return false; }

            dataContext.Message.Remove(msg);

            dataContext.SaveChanges();
            return true;
        }

        public bool UpdateMessage(int idMessage, MessageViewModel message)
        {
            Message? existingMessage = dataContext.Message.FirstOrDefault(x => x.Id == idMessage);

            bool patched = false;

            if (existingMessage == null) { return patched; }

            existingMessage.Sender = dataContext.User.FirstOrDefault(x => x.Email == message.emailSender);
            existingMessage.Receiver = dataContext.User.FirstOrDefault(x => x.Email == message.emailReceiver);
            if (existingMessage.Sender != null && existingMessage.Receiver != null)
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
