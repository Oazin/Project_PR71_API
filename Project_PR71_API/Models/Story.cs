using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class Story
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public User User { get; set; }

        public byte[] Image { get; set; }

        public StoryViewModel Convert()
        {
            return new StoryViewModel
            {
                Id = Id,
                DateTime = DateTime,
                User = User.Convert(),
                Image = Image,
            };
        }
    }
}
