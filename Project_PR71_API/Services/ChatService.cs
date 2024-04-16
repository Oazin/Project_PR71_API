using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Configuration;
using Project_PR71_API.Models;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;
using System.Collections.Generic;

namespace Project_PR71_API.Services
{
    public class ChatService : IChatService
    {
        private readonly DataContext dataContext;
        private readonly IMessageService messageService;

        public ChatService(DataContext dataContext, IMessageService messageService)
        {
            this.dataContext = dataContext;
            this.messageService = messageService;
        }

        /// <summary>
        /// Create a new chat
        /// </summary>
        /// <param name="chatViewModel"></param>
        /// <returns> boolean </returns>
        public int CreateChat(ChatViewModel chatViewModel)
        {
            if (chatViewModel == null) { return 0; }
            if (dataContext.Chat.FirstOrDefault(x => (x.User1.Email == chatViewModel.User1.Email && x.User2.Email == chatViewModel.User2.Email) || (x.User1.Email == chatViewModel.User2.Email && x.User2.Email == chatViewModel.User1.Email)) != null ) { return false; }

            Chat newChat = chatViewModel.Convert();

            newChat.Id = dataContext.Chat.Any() ? dataContext.Chat.Max(x => x.Id) + 1 : 1;
            newChat.User1 = dataContext.User.FirstOrDefault(x => x.Email == chatViewModel.User1.Email);
            newChat.User2 = dataContext.User.FirstOrDefault(x => x.Email == chatViewModel.User2.Email);

            if (newChat.User1 == null || newChat.User2 == null) { return 0; }

            dataContext.Chat.Add(newChat);

            dataContext.SaveChanges();

            return newChat.Id;
        }


        /// <summary>
        /// Delete a chat
        /// </summary>
        /// <param name="idChat"></param>
        /// <returns> boolean </returns>
        public bool DeleteChat(int idChat)
        {
            Chat chat = dataContext.Chat.FirstOrDefault(x =>x.Id == idChat);
            if (chat == null) { return false; }

            dataContext.Chat.Remove(chat);
            dataContext.SaveChanges(); 
            
            return true;
        }

        /// <summary>
        /// Get all chat of a user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ICollection<ChatViewModel> GetChatByEmail(string email)
        {
            ICollection<Chat> chats = dataContext.Chat.Include(x => x.User1).Include(x => x.User2).Where(x => x.User1.Email == email || x.User2.Email == email).ToList();
            ICollection<ChatViewModel> chatViewModels = chats.Select(x => x.Convert()).ToList();

            return chatViewModels;
        }

        public ICollection<int> GetNotification(string emailCurrentUser)
        {
            ICollection<int> idChatList = new List<int>();
            ICollection <ChatViewModel> chats = GetChatByEmail(emailCurrentUser);
            foreach (ChatViewModel chat in chats)
            {
                if (chat != null)
                { 
                    if (messageService.HadReaded(chat.Id, emailCurrentUser))
                    {
                        idChatList.Add(chat.Id);
                    }
                }
            }
            return idChatList;
        }

    }
}
