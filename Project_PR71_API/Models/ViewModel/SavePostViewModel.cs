namespace Project_PR71_API.Models.ViewModel
{
    public class SavePostViewModel
    {
        public int Id { get; set; }

        public PostViewModel Post { get; set; }

        public string emailUser { get; set; }

        public SavePost Convert()
        {
            return new SavePost
            {
                Id = this.Id,
                Post = this.Post.Convert(),
            };
        }
    }
}
