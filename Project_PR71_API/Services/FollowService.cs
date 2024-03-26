using Project_PR71_API.Configuration;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Models;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Services
{
    public class FollowService : IFollowService
    {
        private readonly DataContext dataContext;

        public FollowService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Get all followings of a user
        /// </summary>
        /// <param name="emailUser"></param>
        /// <returns> ICollections of Followers view model </returns>
        public ICollection<FollowViewModel> GetFollowings(string emailUser)
        {
            List<Follow> follows = dataContext.Follow.Where(x => x.FollowerEmail == emailUser).ToList();
            List<FollowViewModel> followViewModels = new List<FollowViewModel>();

            foreach (Follow follow in follows)
            {
                followViewModels.Add(follow.Convert());
            }

            return followViewModels;
        }

        /// <summary>
        /// Get all followers of a user
        /// </summary>
        /// <param name="emailUser"></param>
        /// <returns> ICollections of Followers view model </returns>
        public ICollection<FollowViewModel> GetFollowers(string emailUser)
        {
            List<Follow> follows = dataContext.Follow.Where(x => x.FollowingEmail == emailUser).ToList();
            List<FollowViewModel> followViewModels = new List<FollowViewModel>();

            foreach (Follow follow in follows)
            {
                followViewModels.Add(follow.Convert());
            }

            return followViewModels;
        }

        /// <summary>
        /// Add a follow
        /// </summary>
        /// <param name="followViewModel"></param>
        /// <returns> boolean </returns>
        public bool AddFollow(FollowViewModel followViewModel)
        {
            if (followViewModel == null) { return false; }
            Follow follow = followViewModel.Convert();
            follow.Follower = dataContext.User.FirstOrDefault(x => x.Email == followViewModel.FollowerEmail);
            follow.Following = dataContext.User.FirstOrDefault(x => x.Email == followViewModel.FollowingEmail);

            if (follow.Follower == null || follow.Following == null) { return false; }

            dataContext.Follow.Add(follow);

            dataContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// Unfollow a user
        /// </summary>
        /// <param name="followViewModel"></param>
        /// <returns>boolean </returns>
        public bool UnFollow(FollowViewModel followViewModel)
        {
            if (followViewModel == null) { return false; }
            Follow follow = dataContext.Follow.FirstOrDefault(x => x.FollowingEmail == followViewModel.FollowingEmail);

            dataContext.Follow.Remove(follow);

            dataContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// Check if a user is following another user
        /// </summary>
        /// <param name="followViewModel"></param>
        /// <returns> boolean </returns>
        public bool IsFollow(FollowViewModel followViewModel)
        {
            return dataContext.Follow.FirstOrDefault(x => x.FollowerEmail == followViewModel.FollowerEmail && x.FollowingEmail == followViewModel.FollowingEmail) != null;
        }

    }
}
