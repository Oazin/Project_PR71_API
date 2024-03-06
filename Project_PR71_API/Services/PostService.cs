using Microsoft.Extensions.Hosting;
using Project_PR71_API.Configuration;
using Project_PR71_API.Models;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;
using System.Linq;

namespace Project_PR71_API.Services
{

    public class PostService : IPostService
    {
        private readonly DataContext dataContext;
        private readonly IImageService imageService;

        public PostService(DataContext dataContext, IImageService imageService) 
        {
            this.dataContext = dataContext;
            this.imageService = imageService;
        }

        /// <summary>
        /// Get all Post of a user
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public ICollection<Post> GetPostsByUser(string userEmail)
        {
            return dataContext.Post.Where(x => x.User.Email == userEmail).OrderBy(x => x.DateTime).ToList();
        }
        
        /// <summary>
        /// Add new post 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public bool AddPost(string userEmail, Post post)
        {
            User user = dataContext.User.FirstOrDefault(x => x.Email == userEmail);
            if (user == null) { return false; }

            dataContext.Post.AddAsync(post);
            dataContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Delete post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public bool DeletePost(int postId)
        {
            Post post = dataContext.Post.FirstOrDefault(x => x.Id == postId);
            if (post == null){ return false; }

            post.Images = dataContext.Image.Where(x => x.Id == postId).ToList();

            foreach (Image image in post.Images)
            {
                dataContext.Image.Remove(image);
            }
            
            dataContext.Post.Remove(post);
            dataContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public bool UpdatePost(int postId, Post post)
        {
            Post existingPost = dataContext.Post.FirstOrDefault(x => x.Id == postId);
            bool patched = false;

            if (existingPost == null) { return false; }

            if (existingPost.Description != post.Description)
            {
                existingPost.Description = post.Description;
                patched = true;
            }

            dataContext.SaveChanges();

            return patched;
        }

        /// <summary>
        /// Add new likes
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="likes"></param>
        /// <returns></returns>
        public bool AddLikes(int postId, LikeViewModel newLikeViewModel)
        {
            Post existingPost = dataContext.Post.FirstOrDefault(x => x.Id == postId);
            if (existingPost == null) { return false; }

            Like newLike = newLikeViewModel.Convert();

            newLike.User = dataContext.User.FirstOrDefault(x => x.Email == newLikeViewModel.EmailUser);
            newLike.Post = dataContext.Post.FirstOrDefault(x => x.Id == postId);

            if  (newLike.User == null || newLike.Post == null) { return false; }

            existingPost.Likes.Add(newLike);

            dataContext.Like.Add(newLike);

            dataContext.SaveChanges();

            return true;
        }
    }
}
