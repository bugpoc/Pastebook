using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebook.ViewModels
{
    public class PasswordViewModel
    {
        [Required(ErrorMessage = "The New Password field is required")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The Password must contain at least 8 characters up to 50 characters")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "The Confirm New Password field is required")]
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password do not match")]
        public string ConfirmNewPassword { get; set; }
    }
}