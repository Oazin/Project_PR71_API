using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Models;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {

        private readonly ILogger<StoryController> _logger;
        private readonly IStoryService storyService;

        public StoryController(ILogger<StoryController> logger, IStoryService storyService)
        {
            _logger = logger;
            this.storyService = storyService;
        }

        [HttpGet("stories", Name = "GetStories")]
        public ICollection<StoryViewModel> GetStories()
        {
            return storyService.GetStories();
        }

        [HttpGet("{userEmail}", Name = "GetStoriesByUser")]
        public ICollection<StoryViewModel> GetStorysByUser([FromRoute] string userEmail)
        {
            return storyService.GetStorysByUser(userEmail);
        }

        [HttpPost("{userEmail}", Name = "AddStory")]
        public bool AddStory([FromRoute] string userEmail, [FromBody] StoryViewModel storyViewModel)
        {
            return storyService.AddStory(userEmail, storyViewModel);
        }

        [HttpDelete("{idStory}", Name = "DeleteStory")]
        public bool DeleteStory([FromRoute] int idStory)
        {
            return storyService.DeleteStory(idStory);
        }
    }
}
