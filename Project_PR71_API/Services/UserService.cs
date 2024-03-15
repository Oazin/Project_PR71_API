using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Project_PR71_API.Configuration;
using Project_PR71_API.Models;
using Project_PR71_API.Models.ViewModel;
using Project_PR71_API.Services.IServices;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;

namespace Project_PR71_API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext dataContext;

        public UserService(DataContext dataContext) 
        { 
            this.dataContext = dataContext;
        }

        public UserViewModel? GetUserByEmail(string email)
        {
            User user = this.dataContext.User.FirstOrDefault(x => x.Email == email);
            if (user == null) { return null; }
            return user.Convert();
        }

        public void ConnectUser(string email, string code)
        {
            if (this.dataContext.User.FirstOrDefault(x => x.Email == email) == null)
            {
                this.CreateUser(this.ConverteEmailAdress(email));
            }
            
            SendEmailAsync(email, this.Decrypt(code, "w9H$5aLp2#qJx@8Z"));
        }

        private void CreateUser(string email)
        {
            User user = new User
            {
                Email = email,
                Name = email.Split(".")[1].Split("@")[0].ToUpper(),
                Firstname = email.Split(".")[0].ToLower(),
                Username = email.Split(".")[0].ToLower() + email.Split(".")[1].Split("@")[0].Substring(0,1).ToLower(),
                Picture = GenerateImage(email.Split(".")[0].ToUpper().Substring(0, 1), email.Split(".")[1].ToUpper().Substring(0, 1))
            };

            dataContext.AddAsync(user);

            dataContext.SaveChangesAsync();
        }

        private void SendEmailAsync(string email, string code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("UTGram", "utgram90@gmail.com"));
            message.To.Add(new MailboxAddress("", this.ConverteEmailAdress(email)));
            message.Subject = "UTgram Code Validator";
            message.Body = new TextPart("html") { Text = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Two-Step Authentication</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f6f8fa;\r\n            padding: 20px;\r\n        }\r\n        .container {\r\n            max-width: 600px;\r\n            margin: auto;\r\n            background-color: #fff;\r\n            border-radius: 8px;\r\n            padding: 40px;\r\n            box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.1);\r\n        }\r\n        h2 {\r\n            color: #0366d6;\r\n        }\r\n        p {\r\n            color: #586069;\r\n            font-size: 16px;\r\n        }\r\n        .verification-code {\r\n            font-size: 18px;\r\n            font-weight: bold;\r\n            color: #0366d6;\r\n            margin-bottom: 20px;\r\n        }\r\n        .button {\r\n            background-color: #0366d6;\r\n            color: #fff;\r\n            padding: 10px 20px;\r\n            border: none;\r\n            border-radius: 5px;\r\n            font-size: 16px;\r\n            cursor: pointer;\r\n            text-decoration: none;\r\n        }\r\n        .button:hover {\r\n            background-color: #005cc5;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h2>Two-Step Authentication</h2>\r\n        <p>Please use the following verification code to complete the authentication process:</p>\r\n        <p class=\"verification-code\">" + code + "</p>\r\n        <p>If you did not request this code, please ignore this email.</p>\r\n        <p>Thank you,<br>UTgram Team</p>\r\n    </div>\r\n</body>\r\n</html>\r\n" };


            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("utgram90@gmail.com", "fktg oumg wdgx jteb");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        private string ConverteEmailAdress(string email)
        {
            return email.Split('@')[0] + "@utbm.fr";
        }

        
        public byte[] GenerateImage(string fnChar, string lnChar)
        {
            // Create a new bitmap with specified dimensions
            int size = 200; // Size of the square
            Bitmap bitmap = new Bitmap(size, size);

            // Create a Graphics object from the bitmap
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Generate a random color for the background
                Random random = new Random();
                Color bgColor = Color.FromArgb(155,random.Next(256), random.Next(256), random.Next(256));

                // Fill the background with the random color
                graphics.Clear(bgColor);

                // Draw the text "BC" in the center of the square
                Font font = new Font("Arial", 80, FontStyle.Bold);
                Brush brush = Brushes.Black; // You can choose any color for the text
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                graphics.DrawString(fnChar+lnChar, font, brush, new RectangleF(0, 0, size, size), format);
            }

            byte[] imageBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                imageBytes = memoryStream.ToArray();
            }
            return imageBytes;
        }

        public bool UpdateUser(string email, UserViewModel userViewModel)
        {
            User user = userViewModel.Convert();

            if (user == null) { return false;  }
            User existingUser = dataContext.User.FirstOrDefault(x => x.Email == email);

            if (existingUser == null) { return false; }

            bool patched = false;

            if (existingUser.Username != user.Username && !string.IsNullOrWhiteSpace(user.Username))
            {
                existingUser.Username = user.Username;
                patched = true;
            }

            if (existingUser.Bio !=  user.Bio && !string.IsNullOrWhiteSpace(user.Bio))
            {
                existingUser.Bio = user.Bio;
                patched = true;
            }

            if (existingUser.Picture != user.Picture && user.Picture.Length != 0)
            {
                existingUser.Picture = user.Picture;
                patched = true;
            }

            dataContext.SaveChanges();

            return patched;
        }

        
        private string Decrypt(string cipherText, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 16));
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public ICollection<UserViewModel>? ResearchUsers(string searchTerms)
        {
            if (!string.IsNullOrEmpty(searchTerms))
            {
                ICollection<User> users = dataContext.User
                    .Where(x => x.Username.ToUpper().Contains(searchTerms.ToUpper()) ||
                                x.Name.ToUpper().Contains(searchTerms.ToUpper()) ||
                                x.Firstname.ToUpper().Contains(searchTerms.ToUpper()))
                    .OrderBy(x => x.Username.ToUpper())
                    .Take(10)
                    .ToList();

                return users.Select(x => x.Convert()).ToList();
            }

            return null;
        }

    }

}