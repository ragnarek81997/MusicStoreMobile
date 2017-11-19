using MusicStoreMobile.Core.Converters;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreMobile.Core.Models
{
    public class ApplicationUserModel : BaseEntity
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(24, MinimumLength = 24)]
        public string PhotoId { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string LastName { get; set; }

        public string Password { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime? AccessTokenExpires { get; set; }
    }


}
