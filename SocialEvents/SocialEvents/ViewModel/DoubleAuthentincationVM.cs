using SocialEvents.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialEvents.ViewModel
{
    public class DoubleAuthentincationVM
    {
        public User Usr { get; set; }
        public int number { get; set; }
        [Required(ErrorMessage = "Required field")]
        public int number2 { get; set; }
        public DoubleAuthentincationVM() { }
            public DoubleAuthentincationVM(User u,int n)
        {
            Usr = new User { Email = u.Email, Password = u.Password };
            
            number = n;
          
        }
    }
}