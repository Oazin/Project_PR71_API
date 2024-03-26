using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Configuration;
using Project_PR71_API.Models;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Services
{
    public class SavePostService : ISavePostService
    {
        private readonly DataContext dataContext;

        public SavePostService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <summary>
        /// Add a post to saved post
        /// </summary>
        /// <param name="email"></param>
        /// <param name="idPost"></param>
        /// <returns> boolean </returns>
        public bool AddSavePost(string email, int idPost)
        {
            Post post = dataContext.Post.FirstOrDefault(x => x.Id == idPost);
            User user = dataContext.User.FirstOrDefault(x => x.Email == email);

            if (post == null || user == null) { return false; }

            SavePost savePost = new SavePost
            {
                Id = dataContext.SavePost.Any() ? dataContext.SavePost.Max(x => x.Id) + 1 : 1,
                Post = post,
                User = user
            };

            dataContext.SavePost.Add(savePost);
            dataContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete a post from saved post
        /// </summary>
        /// <param name="email"></param>
        /// <param name="idPost"></param>
        /// <returns> boolean </returns>
        public bool DeleteSavePost(string email, int idPost)
        {
            SavePost savePost = dataContext.SavePost.FirstOrDefault(x => x.Post.Id == idPost && x.User.Email == email);
            if (savePost == null) { return false; }

            dataContext.SavePost.Remove(savePost);
            dataContext.SaveChanges();

            return true;
        }

        /// <summary>
        /// get all saved post of a user
        /// </summary>
        /// <param name="email"></param>
        /// <returns>ICollections of post view model</returns>
        public ICollection<SavePostViewModel>? GetSavePostByEmail(string email)
        {
            ICollection<SavePost> savePosts = dataContext.SavePost.Include(x => x.User).Include(x => x.Post).Where(x => x.User.Email == email).OrderByDescending(x => x.Id).ToList();
            foreach (var savePost in savePosts)
            {
                savePost.Post = dataContext.Post.Include(x => x.User).Include(x => x.Images).Include(x => x.Likes).FirstOrDefault(x => x.Id == savePost.Post.Id);
            }
            ICollection<SavePostViewModel> savePostViewModel = savePosts.Select(x => x.Convert()).ToList();

            return savePostViewModel;
        }

        /// <summary>
        /// Check if a post is saved by a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="idPost"></param>
        /// <returns> boolean </returns>
        public bool HadSaved(string email, int idPost)
        {
            SavePost savePost = dataContext.SavePost.FirstOrDefault(x => x.Post.Id == idPost && x.User.Email == email);
            return savePost != null;
        }
    }
}
