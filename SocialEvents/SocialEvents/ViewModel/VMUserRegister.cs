using SocialEvents.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialEvents.ViewModel
{
    public class VMUserRegister
    {
        [Required(ErrorMessage = "Required field")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "אותיות באנגלית, לפחות אחת גדולה ולפחות אחת קטנה, לפחות ספרה אחת")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "At least 8, at most 20")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Required field")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "אותיות באנגלית, לפחות אחת גדולה ולפחות אחת קטנה, לפחות ספרה אחת")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "At least 8, at most 20")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Enter password again")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Required field")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
    }
}