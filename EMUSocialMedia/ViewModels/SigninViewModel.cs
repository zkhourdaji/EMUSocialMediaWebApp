using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace EMUSocialMedia.ViewModels
{
    public class SigninViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
