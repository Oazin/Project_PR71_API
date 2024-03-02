using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Models
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] Picture { get; set; }

        public int Index { get; set; }

        public Post Post { get; set; }

        public ImageViewModel Convert()
        {
            return new ImageViewModel
            {
                Id = Id,
                Picture = Picture,
                Index = Index,
                idPost = Post.Id,
            };
        }
    }
}
