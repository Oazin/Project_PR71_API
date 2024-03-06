namespace Project_PR71_API.Models.ViewModel
{
    public class UserViewModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Firstname { get; set; }

        public string? Bio { get; set; }

        public byte[]? Picture { get; set; }

        public string? Username { get; set; }

        public User Convert()
        {
            return new User
            {
                Email = Email,
                Name = Name,
                Firstname = Firstname,
                Bio = Bio,
                Picture = Picture,
                Username = Username,
            };
        }
    }
}
