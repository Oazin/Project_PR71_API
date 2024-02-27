using Microsoft.Extensions.Hosting;
using Project_PR71_API.Configuration;
using Project_PR71_API.Models;

namespace Project_PR71_API.Services
{

    public class PostService : IPostService
    {
        private readonly DataContext dataContext;

        public PostService(DataContext dataContext) 
        {
            this.dataContext = dataContext;
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
            if (user == null) { }

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
        public bool AddLikes(int postId, int likes)
        {
            Post existingPost = dataContext.Post.FirstOrDefault(x => x.Id == postId);
            if (existingPost == null) { return false; }

            existingPost.Like = likes;

            dataContext.SaveChanges();

            return true;
        }
    }
}
