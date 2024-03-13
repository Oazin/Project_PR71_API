using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Project_PR71_API.Configuration;
using Project_PR71_API.Models;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;
using System.Linq;
using System.Xml.Linq;

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

        public ICollection<PostViewModel> GetFeed()
        {
            ICollection<Post> posts = dataContext.Post.Include(x => x.User).Include(x => x.Images).Include(x => x.Comments).Include(x => x.Likes).OrderByDescending(x => x.DateTime).Take(20).ToList();
            ICollection<PostViewModel> postsViewModel = posts.Select(x => x.Convert()).ToList();

            return postsViewModel;
        }

        /// <summary>
        /// Get all Post of a user
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public ICollection<PostViewModel> GetPostsByUser(string userEmail)
        {
            ICollection<Post> posts = dataContext.Post.Include(x => x.User).Include(x => x.Images).Include(x => x.Comments).Include(x => x.Likes).Where(x => x.User.Email == userEmail).OrderByDescending(x => x.DateTime).ToList();
            ICollection<PostViewModel> postsViewModel = posts.Select(x => x.Convert()).ToList();

            return postsViewModel;
        }
        
        /// <summary>
        /// Add new post 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public bool AddPost(string userEmail, PostViewModel postViewModel)
        {
            User user = dataContext.User.FirstOrDefault(x => x.Email == userEmail);
            if (user == null || postViewModel == null) { return false; }

            Post post = postViewModel.Convert();
            post.User = user;
            post.Id = dataContext.Post.Any() ? dataContext.Post.Max(x => x.Id) + 1 : 1;

            dataContext.Post.Add(post);

            dataContext.SaveChanges();

            foreach (var image in postViewModel.Images)
            {
                image.idPost = post.Id;
                if (!this.imageService.AddImage(image)) { 
                    this.DeletePost(post.Id);
                    return false;
                }
            }

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

            post.Images = dataContext.Image.Where(x => x.Post.Id == postId).ToList();
            post.Comments = dataContext.Comment.Where(x =>x.Post.Id == postId).ToList();
            post.Likes = dataContext.Like.Where(x => x.Post.Id == postId).ToList();

            foreach (Image image in post.Images)
            {
                dataContext.Image.Remove(image);
            }

            foreach (Comment comment in post.Comments)
            {
                dataContext.Comment.Remove(comment);
            }

            foreach (Like like in post.Likes)
            {
                dataContext.Like.Remove(like);
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
            newLike.Id = dataContext.Like.Any() ? dataContext.Like.Max(x => x.Id) + 1 : 1;

            newLike.User = dataContext.User.FirstOrDefault(x => x.Email == newLikeViewModel.EmailUser);
            newLike.Post = dataContext.Post.FirstOrDefault(x => x.Id == postId);

            if  (newLike.User == null || newLike.Post == null) { return false; }

            dataContext.Like.Add(newLike);

            dataContext.SaveChanges();

            return true;
        }

        public bool HadLiked(int idPost, string emailUser)
        {
            return (dataContext.Like.FirstOrDefault(x => x.Post.Id == idPost && x.User.Email.Equals(emailUser)) != null);
        }

        public bool DeleteLike(int idPost, string emailUser)
        {
            Like like = dataContext.Like.FirstOrDefault(x => x.Post.Id == idPost && x.User.Email.Equals(emailUser));

            if (like == null) { return false; }

            dataContext.Like.Remove(like);

            dataContext.SaveChanges();

            return true;
        }

    }
}
