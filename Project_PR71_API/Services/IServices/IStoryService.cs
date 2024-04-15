using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Services.IServices
{
    public interface IStoryService
    {
        public bool AddStory(string userEmail, StoryViewModel storyViewModel);
        public bool DeleteStory(int idStory);
        public ICollection<StoryViewModel> GetStories();
        public ICollection<StoryViewModel> GetStorysByUser(string userEmail);
    }
}
