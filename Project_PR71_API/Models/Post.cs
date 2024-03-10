using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime DateTime { get; set; }

        public User User { get; set; }

        public ICollection<Image>? Images { get; set;}

        public ICollection<Comment>? Comments { get; set;}

        public ICollection<Like>? Likes { get; set; }

        public PostViewModel Convert()
        {
            return new PostViewModel
            {
                Id = Id,
                Title = Title,
                Description = Description,
                DateTime = DateTime,
                User = User.Convert(),
                Images = Images.Select(x => x.Convert()).ToList(),
                Comments = Comments.Select(x => x.Convert()).ToList(),
                Likes = Likes.Select(x => x.Convert()).ToList(),
            };
        }
    }
}
