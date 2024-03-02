using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class Message
    {
        public int Id { get; set; }

        public User Sender { get; set; }

        public User Receiver { get; set; }

        public string Content {  get; set; }

        public MessageViewModel Convert()
        {
            return new MessageViewModel
            {
                Id = Id,
                emailReceiver = Receiver.Email,
                emailSender = Sender.Email,
                Content = Content,
            };
        }
    }
}
