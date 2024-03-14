using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public User Writer { get; set; }

        public Post Post { get; set; }

        public string Content { get; set; }

        public CommentViewModel Convert()
        {
            return new CommentViewModel
            {
                Id = Id,
                Writer = Writer.Convert(),
                idPost = Post.Id,
                Content = Content,
            }; 
        }
    }
}
