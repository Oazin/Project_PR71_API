namespace Project_PR71_API.Models.ViewModel
{
    public class StoryViewModel
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public UserViewModel User { get; set; }

        public byte[] Image { get; set; }

        public Story Convert()
        {
            return new Story
            {
                Id = Id,
                DateTime = DateTime,
                User = User.Convert(),
                Image = Image,
            };
        }
    }
}
