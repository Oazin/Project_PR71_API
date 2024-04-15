using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Services.IServices
{
    public interface IChatService
    {
        public bool CreateChat(ChatViewModel chatViewModel);

        public ICollection<ChatViewModel> GetChatByEmail(string email);

        public bool DeleteChat(int idChat);

        public ICollection<int> GetNotification(string emailCurrentUser);
    }
}
