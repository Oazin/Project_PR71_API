using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Models;
using Project_PR71_API.Configuration;
using Project_PR71_API.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Project_PR71_API.Services
{
    public class StoryService : IStoryService
    {
        private readonly DataContext dataContext;

        public StoryService(DataContext dataContext) 
        { 
            this.dataContext = dataContext;
        }

        public bool AddStory(string userEmail, StoryViewModel storyViewModel)
        {
            if (storyViewModel == null) { return false; }
            User user = dataContext.User.FirstOrDefault(x => x.Email == userEmail);
            if (user == null) { return false; }
            Story newStory = storyViewModel.Convert();
            newStory.User = user;
            newStory.Id = dataContext.Story.Any() ? dataContext.Story.Max(x => x.Id) + 1 : 1;

            dataContext.Story.Add(newStory);
            dataContext.SaveChanges();

            return true;
        }

        public bool DeleteStory(int idStory)
        {
            Story story = dataContext.Story.FirstOrDefault(x => x.Id == idStory);
            if (story == null) { return false; }

            dataContext.Story.Remove(story);
            dataContext.SaveChanges();
            return true;
        }

        public ICollection<StoryViewModel> GetStories()
        {
            ICollection<Story> stories = dataContext.Story.Include(x => x.User).OrderByDescending(x => x.DateTime).Take(20).ToList();
            ICollection<StoryViewModel> storiesViewModel = stories.Select(x => x.Convert()).ToList();
            return storiesViewModel;
        }

        public ICollection<StoryViewModel> GetStorysByUser(string userEmail)
        {
            ICollection<Story> stories = dataContext.Story.Include(x => x.User).OrderByDescending(x => x.DateTime).Where(x => x.User.Email == userEmail).ToList();
            ICollection<StoryViewModel> storiesViewModel = stories.Select(x => x.Convert()).ToList();
            return storiesViewModel;
        }
    }
}
