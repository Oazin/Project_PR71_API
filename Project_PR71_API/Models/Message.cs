using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class Message
    {
        public int Id { get; set; }

        public Chat Chat { get; set; }

        public User Sender { get; set; }

        public string Content {  get; set; }

        public MessageViewModel Convert()
        {
            return new MessageViewModel
            {
                Id = Id,
                IdChat = Chat.Id,
                emailSender = Sender.Email,
                Content = Content,
            };
        }
    }
}
