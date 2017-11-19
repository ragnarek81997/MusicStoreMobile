using MvvmCross.Core.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreMobile.Core.Models
{
    public class BaseEntity
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        public string Id { get; set; }

    }
}
