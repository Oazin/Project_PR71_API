using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Services.IServices
{
    public interface IImageService
    {
        public ICollection<ImageViewModel> GetImageByPost(int idPost);

        public bool DeleteImage(int idImage);

        public bool AddImage(ImageViewModel image);
    }
}
