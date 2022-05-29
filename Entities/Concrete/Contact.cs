using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Contact
    {

        [Key]
        public int ContactID { get; set; }

        
        public string UserName { get; set; }

      
        public string UserMail { get; set; }

        
        public string Subject { get; set; }

        
        public string Message { get; set; }


        public DateTime Date { get; set; }

        public bool ContactStatus { get; set; }


    }
}
