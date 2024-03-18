using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public User User1 { get; set; }

        public User User2 { get; set; }

        public ICollection<Message>? Messages { get; set; }

        public ChatViewModel Convert()
        {
            return new ChatViewModel
            {
                Id = Id,
                User1 = User1.Convert(),
                User2 = User2.Convert(),
            };
        }
    }
}
