using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Configuration;
using Project_PR71_API.Models;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Jobs
{
    public class StoryJob
    {
        private readonly IStoryService storyService;
        private readonly ILogger<StoryJob> logger;
        private readonly DataContext dataContext;

        public StoryJob(IStoryService storyService, ILogger<StoryJob> logger, DataContext dataContext)
        {
            this.storyService = storyService;
            this.logger = logger;
            this.dataContext = dataContext;
        }

        public void Execute()
        {
            logger.LogInformation("Start StoryJob");
            ICollection<Story> stories = dataContext.Story.Include(x => x.User).OrderByDescending(x => x.DateTime).ToList();
            foreach (Story story in stories)
            {
                if (story.DateTime.AddDays(1) < DateTime.Now)
                {
                    storyService.DeleteStory(story.Id);
                }
            }
            logger.LogInformation("End StoryJob");
        }
    }
}
