using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WriterProject.Models
{
    public class AdminLoginViewModel
    {

        [Required(ErrorMessage ="E-posta alanı boş olamaz!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş olamaz!")]
        public string password { get; set; }


    }
}