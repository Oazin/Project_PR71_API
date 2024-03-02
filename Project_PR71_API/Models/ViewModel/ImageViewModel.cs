namespace Project_PR71_API.Models.ViewModel
{
    public class ImageViewModel
    {
        public int Id { get; set; }

        public byte[] Picture { get; set; }

        public int Index { get; set; }

        public int idPost { get; set; }

        public Image Convert()
        {
            return new Image
            {
                Id = Id,
                Picture = Picture,
                Index = Index,
            };
        }
    }
}
