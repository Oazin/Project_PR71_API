using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SavePostController : ControllerBase
    {
        private readonly ISavePostService savePostService;

        public SavePostController( ISavePostService savePostService)
        {
            this.savePostService = savePostService;
        }

        [HttpGet("{email}", Name = "GetSavePostByEmail")]
        public ICollection<SavePostViewModel>? GetSavePostByEmail([FromRoute] string email)
        {
            return savePostService.GetSavePostByEmail(email);
        }

        [HttpGet("{email}/{idPost}", Name = "HadSaved")]
        public bool HadSaved([FromRoute] string email, [FromRoute] int idPost)
        {
            return savePostService.HadSaved(email, idPost);
        }

        [HttpPost("{email}/{idPost}", Name = "AddSavePost")]
        public bool AddSavePost([FromRoute] string email, [FromRoute] int idPost)
        {
            return savePostService.AddSavePost(email, idPost);
        }

        [HttpDelete("{email}/{idPost}", Name = "DeleteSavePost")]
        public bool DeleteSavePost([FromRoute] string email, [FromRoute] int idPost)
        {
            return savePostService.DeleteSavePost(email, idPost);
        }

    }
}
