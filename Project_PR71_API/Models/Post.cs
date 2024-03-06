using System.Reflection.Metadata;

namespace Project_PR71_API.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime DateTime { get; set; }

        public User User { get; set; }

        public ICollection<Image> Images { get; set;}

        public ICollection<Comment> Comments { get; set;}

        public ICollection<Like> Likes { get; set; }
    }
}
