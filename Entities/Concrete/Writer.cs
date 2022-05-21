using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Writer
    {

        [Key]
        public int WriterID { get; set; }

       
        public string WriterName { get; set; }

       
        public string WriterSurName { get; set; }

       
        public string WriterImage { get; set; }

       
        public string WriterMail { get; set; }

       
        public string WriterPassword { get; set; }

        public string WriterDescription { get; set; }   

        public bool WriterStatus { get; set; }

        public ICollection<Heading> Headings { get; set; }

        public ICollection<Content> Contents { get; set; }


    }
}
