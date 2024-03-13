namespace Project_PR71_API.Models.ViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public User Writer { get; set; }

        public int idPost { get; set; }

        public string Content { get; set; }

        public Comment Convert()
        {
            return new Comment
            {
                Id = Id,
                Content = Content,
                Writer = Writer,
            };
        }
    }
}
