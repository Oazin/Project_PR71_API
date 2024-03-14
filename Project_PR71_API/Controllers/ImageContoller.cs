using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;
namespace Project_PR71_API.Controllers
{
    [Route("api/Image")]
    public class ImageContoller : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageContoller(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet("{idPost}")]
        public ICollection<ImageViewModel> GetImageByPost([FromQuery] int idPost)
        {
            return imageService.GetImageByPost(idPost);
        }

        [HttpPost]
        public bool AddImage([FromQuery] ImageViewModel imageViewModel)
        {
            return imageService.AddImage(imageViewModel);
        }

        [HttpDelete("{idPost}")]
        public bool DeleteImage([FromQuery] int idPost)
        {
            return imageService.DeleteImage(idPost);
        }
    }
}
