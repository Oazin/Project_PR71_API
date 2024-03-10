namespace Project_PR71_API.Models.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime DateTime { get; set; }

        public UserViewModel User { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }

        public ICollection<CommentViewModel>? Comments { get; set; }

        public ICollection<LikeViewModel>? Likes { get; set; }

        public Post Convert()
        {
            return new Post
            {
                Id = Id,
                Title = Title,
                Description = Description,
                DateTime = DateTime,
            };

        }
    }
}
