using System.Reflection.Metadata;

namespace Project_PR71_API.Models
{
    public class Post
    {
        public int Id { get; set; }

        public int Like { get; set; }

        public string? Description { get; set; }

        public byte[] Picture { get; set; }

        public DateTime DateTime { get; set; }

        public User User { get; set; }
    }
}
