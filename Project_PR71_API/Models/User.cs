using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class User
    {
        public string Email {  get; set; }

        public string Name { get; set; }

        public string Firstname { get; set; }

        public string? Bio { get; set; }

        public byte[]? Picture { get; set; }

        public string? Username { get; set; }

        public ICollection<Post>? Posts { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public virtual ICollection<Follow>? Followers { get; set; }

        public virtual ICollection<Follow>? Followings { get; set; }

        public ICollection<Like>? Likes { get; set; }

        public ICollection<SavePost>? SavePosts { get; set; }

        public ICollection<Story> Stories { get; set; }

        public UserViewModel Convert()
        {
            return new UserViewModel
            {
                Email = Email,
                Name = Name,
                Firstname = Firstname,
                Bio = Bio,
                Picture = Picture,
                Username = Username,
            };
        }
    }
}
