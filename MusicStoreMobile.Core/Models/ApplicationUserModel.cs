using MusicStoreMobile.Core.Converters;
using Newtonsoft.Json;
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
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = ".expires")]
        public DateTime? AccessTokenExpires { get; set; }

        //public bool IsOnline { get; set; }
        //public DateTime? OnlineDate { get; set; }
    }


}
