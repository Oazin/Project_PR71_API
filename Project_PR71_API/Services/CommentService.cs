using Microsoft.EntityFrameworkCore;
using Project_PR71_API.Configuration;
using Project_PR71_API.Models;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;

namespace Project_PR71_API.Services
{
    public class CommentService : ICommentService
    {
        private readonly DataContext dataContext;

        public CommentService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool AddComment(CommentViewModel commentViewModel)
        {
            if (commentViewModel == null) { return false; }
            Comment comment = commentViewModel.Convert();
            comment.Id = dataContext.Comment.Any() ? dataContext.Comment.Max(x => x.Id) + 1  : 1;
            comment.Writer = dataContext.User.FirstOrDefault(x => x.Email == commentViewModel.Writer.Email);
            comment.Post = dataContext.Post.FirstOrDefault(x => x.Id == commentViewModel.idPost);

            if (comment.Writer == null || comment.Post == null) { return false; }

            dataContext.Comment.Add(comment);

            dataContext.SaveChanges();

            return true;
        }

        public ICollection<CommentViewModel> GetCommentsByPost(int idPost)
        {
            List<Comment> comments = dataContext.Comment.Include(c => c.Writer).Include(c => c.Post).Where(x => x.Post.Id == idPost).OrderBy(x => x.Id).ToList();
            List<CommentViewModel> commentViewModels = comments.Select(x => x.Convert()).ToList();

            return commentViewModels;
        }

        public bool DeleteComment(int idComment)
        {
            Comment comment = dataContext.Comment.FirstOrDefault(x => x.Id == idComment);

            if (comment == null) { return false;}

            dataContext.Comment.Remove(comment);

            dataContext.SaveChanges();

            return true;
        }
    }
}
