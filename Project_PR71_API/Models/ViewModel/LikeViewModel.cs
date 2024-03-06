namespace Project_PR71_API.Models.ViewModel
{
    public class LikeViewModel
    {
        public int Id { get; set; }

        public int IdPost { get; set; }

        public string EmailUser { get; set; }

        public Like Convert()
        {
            return new Like
            {
                Id = Id,
            };
        }
    }
}
