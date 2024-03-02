using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Services.IServices
{
    public interface IFollowService
    {
        public ICollection<FollowViewModel> GetFollowings(string emailUser);

        public ICollection<FollowViewModel> GetFollowers(string emailUser);

        public bool AddFollow(FollowViewModel follow);
        
        public bool UnFollow(FollowViewModel follow);
    }
}
