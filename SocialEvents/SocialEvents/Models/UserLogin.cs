using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
  
namespace SocialEvents.Models
{
    public class UserLogin {
        [Key]
        [Required(ErrorMessage = "Required field")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required field")]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "אותיות באנגלית, לפחות אחת גדולה ולפחות אחת קטנה, לפחות ספרה אחת")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "At least 8, at most 20")]
        public string Password { get; set; }
    }  
}

