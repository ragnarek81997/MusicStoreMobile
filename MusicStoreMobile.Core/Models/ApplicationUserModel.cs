using System;
namespace MusicStoreMobile.Core.Models
{
    public class ApplicationUserModel : BaseEntity
    {
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsOnline { get; set; }
        public DateTime? OnlineDate { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpires { get; set; }
        
    }
}
