using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class SavePost
    {
        public int Id { get; set; }
        
        public Post Post { get; set; }

        public User User { get; set; }

        public SavePostViewModel Convert()
        {
            return new SavePostViewModel
            {
                Id = this.Id,
                Post = this.Post.Convert(),
                emailUser = this.User.Email,
            };
        }
    }
}
