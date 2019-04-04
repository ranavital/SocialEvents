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
     
        [StringLength(100, MinimumLength = 8, ErrorMessage = "At least 8, at most 20")]
        public string Password { get; set; }
    }  
}

