using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialEvents.ViewModel
{
    public class SituationViewModel
    {
        [Required]
        public int SolutionID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CorrectAnswer { get; set; }
        [Required]
        public string IncorrectAnswer1 { get; set; }
        [Required]
        public string IncorrectAnswer2 { get; set; }
        [Required]
        public string CorrectDescription { get; set; }
        [Required]
        public string IncorrectDescription1 { get; set; }
        [Required]
        public string IncorrectDescription2 { get; set; }
        [Required]
        public byte[] Image { get; set; }
    }
}