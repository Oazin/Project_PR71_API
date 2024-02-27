namespace Project_PR71_API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string email {  get; set; }

        public string Name { get; set; }

        public string Fistname { get; set; }

        public string? Bio { get; set; }

        public byte[] Picture { get; set; }

        public string? Username { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
