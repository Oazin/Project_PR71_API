using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class Follow
    {
        public string FollowingEmail { get; set; }
        public string FollowerEmail { get; set; }

        public User Following { get; set; }
        public User Follower { get; set; }

        public FollowViewModel Convert()
        {
            return new FollowViewModel
            {
                FollowerEmail = FollowerEmail,
                FollowingEmail = FollowingEmail,
            };
        }
    }
}

