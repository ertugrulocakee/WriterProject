using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WriterProject.Models
{
    public class WriterLoginViewModel
    {
        [Required(ErrorMessage ="E-posta adresi boş olamaz!")]
        public string WriterMail { get; set; }

        [Required(ErrorMessage = "Şifre boş olamaz!")]
        public string WriterPassword { get; set; }  

    }
}