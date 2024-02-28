namespace Project_PR71_API.Models
{
    public class User
    {
        public string Email {  get; set; }

        public string Name { get; set; }

        public string Firstname { get; set; }

        public string? Bio { get; set; }

        public byte[]? Picture { get; set; }

        public string? Username { get; set; }

        public ICollection<Post>? Posts { get; set; }
    }
}
