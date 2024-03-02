using Project_PR71_API.Configuration;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Models;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Services
{
    public class ImageService : IImageService
    {
        private readonly DataContext dataContext;

        public ImageService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public ICollection<ImageViewModel> GetImageByPost(int idPost)
        {
            List<Image> images = dataContext.Image.Where(x => x.Post.Id == idPost).ToList();
            List<ImageViewModel> imageViewModels = new List<ImageViewModel>();

            foreach (Image image in images)
            {
                imageViewModels.Add(image.Convert());
            }
            
            return imageViewModels;
        }

        public bool DeleteImage(int idImage)
        {
            Image image = dataContext.Image.FirstOrDefault(x => x.Id == idImage);
            dataContext.Image.Remove(image);
            dataContext.SaveChanges();
            return true;
        }

        public bool AddImage(ImageViewModel imageVIewModel)
        {
            if (imageVIewModel == null) { return false; }
            Image image = imageVIewModel.Convert();
            image.Post = dataContext.Post.FirstOrDefault(x => x.Id == imageVIewModel.idPost);

            dataContext.Image.AddAsync(image);

            dataContext.SaveChangesAsync();
            
            return true;
        }
    }
}
