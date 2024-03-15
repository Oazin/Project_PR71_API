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

        public bool UnFollow(FollowViewModel followViewModel)
        {
            if (followViewModel == null) { return false; }
            Follow follow = dataContext.Follow.FirstOrDefault(x => x.FollowingEmail == followViewModel.FollowingEmail);

            dataContext.Follow.Remove(follow);

            dataContext.SaveChanges();

            return true;
        }

        public bool IsFollow(FollowViewModel followViewModel)
        {
            return dataContext.Follow.FirstOrDefault(x => x.FollowerEmail == followViewModel.FollowerEmail && x.FollowingEmail == followViewModel.FollowingEmail) != null;
        }

    }
}
