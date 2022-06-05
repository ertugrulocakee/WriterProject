using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WriterProject.Models
{
    public class WriterLoginViewModel
    {
        [Required]
        public string WriterMail { get; set; }

        [Required]
        public string WriterPassword { get; set; }  

    }
}