
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WriterProject.Models
{
    public class WriterViewModel
    {

        public int id { get; set; } 
        public string WriterName { get; set; }


        public string WriterSurName { get; set; }


        public HttpPostedFileBase Image { get; set; }

        public string ImagePath { get; set; }


        public string WriterMail { get; set; }


        public string WriterPassword { get; set; }

        public string WriterDescription { get; set; }


    }
}