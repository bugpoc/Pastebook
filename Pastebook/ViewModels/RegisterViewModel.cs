using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using PastebookEntityLibrary;

namespace Pastebook.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The Userame maximum length is 50 characters only")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"[a-zA-Z\s,.'-]+", ErrorMessage = "The First Name must contain only letters, spaces, \",\", \".\" and \"-\"")]
        [StringLength(50, ErrorMessage = "The First Name maximum length is 50 characters only")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"[a-zA-Z\s,.'-]+", ErrorMessage = "The Last Name must contain only letters, spaces, \",\", \".\" and \"-\"")]
        [StringLength(50, ErrorMessage = "The Last Name maximum length is 50 characters only")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "The Email Address maximum length is 50 characters only")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The Password must contain at least 8 characters up to 50 characters")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public int? Country { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(50, ErrorMessage = "The Mobile Number maximum length is 50 characters only")]
        public string MobileNumber { get; set; }

        public string Salt { get; set; }
    }
}