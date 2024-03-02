namespace Project_PR71_API.Models.ViewModel
{
    public class FollowViewModel
    {
        public string FollowingEmail { get; set; }
        public string FollowerEmail { get; set; }

        public Follow Convert()
        {
            return new Follow
            {
                FollowerEmail = FollowerEmail,
                FollowingEmail = FollowingEmail,
            };
        }
    }
}
