namespace Project_PR71_API.Models
{
    public class Follow
    {
        public string FollowingEmail { get; set; } // Assuming string type for User's primary key
        public string FollowerEmail { get; set; } // Assuming string type for User's primary key

        public User Following { get; set; }
        public User Follower { get; set; }
    }
}

