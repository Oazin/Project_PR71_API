namespace Project_PR71_API.Models.ViewModel
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        public string emailSender { get; set; }

        public string emailReceiver { get; set; }

        public string Content { get; set; }

        public Message Convert()
        {
            return new Message
            {
                Id = Id,
                Content = Content,
            };
        }
    }
}
