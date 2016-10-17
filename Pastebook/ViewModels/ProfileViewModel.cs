using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.ViewModels
{
    public class ProfileViewModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        public string Gender { get; set; }

        //[Display(Name = "Profile Picture")]
        //public byte[] ProfilePicture { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "About Me")]
        public string AboutMe { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }
}