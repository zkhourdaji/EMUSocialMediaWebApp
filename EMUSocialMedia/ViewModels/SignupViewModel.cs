using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace EMUSocialMedia.ViewModels
{
    public class SignupViewModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }


    }
}
