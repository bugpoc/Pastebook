using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pastebook.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"[\w\s,.'-]+", ErrorMessage = "First Name must contain only letters, spaces, \",\", \".\" and \"-\"")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public List<string> Country { get; set; }

        public string MobileNumber { get; set; }
    }
}