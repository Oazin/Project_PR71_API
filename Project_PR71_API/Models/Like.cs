using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class Like
    {
        public int Id { get; set; }

        public Post Post { get; set; }

        public User User { get; set; }

        public LikeViewModel Convert()
        {
            return new LikeViewModel
            {
                Id = Id,
                IdPost = Post.Id,
                EmailUser = User.Email
            };
        }
    }
}
