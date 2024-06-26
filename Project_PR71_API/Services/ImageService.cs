﻿using Project_PR71_API.Configuration;
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

        /// <summary>
        /// Get all images of a post
        /// </summary>
        /// <param name="idPost"></param>
        /// <returns> ICollections of Images view model </returns>
        public ICollection<ImageViewModel> GetImageByPost(int idPost)
        {
            List<Image> images = dataContext.Image.Where(x => x.Post.Id == idPost).ToList();
            List<ImageViewModel> imageViewModels = images.Select(x => x.Convert()).ToList();
            
            return imageViewModels;
        }

        /// <summary>
        /// Delete an image
        /// </summary>
        /// <param name="idImage"></param>
        /// <returns> boolean </returns>
        public bool DeleteImage(int idImage)
        {
            Image image = dataContext.Image.FirstOrDefault(x => x.Id == idImage);
            dataContext.Image.Remove(image);
            dataContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Add an image
        /// </summary>
        /// <param name="imageVIewModel"></param>
        /// <returns> boolean </returns>
        public bool AddImage(ImageViewModel imageVIewModel)
        {
            if (imageVIewModel == null) { return false; }
            Image image = imageVIewModel.Convert();
            image.Id = dataContext.Image.Any() ? dataContext.Image.Max(x => x.Id) + 1 : 1;
            image.Post = dataContext.Post.FirstOrDefault(x => x.Id == imageVIewModel.idPost);

            dataContext.Image.Add(image);

            dataContext.SaveChanges();
            
            return true;
        }
    }
}
