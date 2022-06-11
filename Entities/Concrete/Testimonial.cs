using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Testimonial
    {

        [Key]
        public int TestimonialID { get; set; }  

        public string TestimonialValue { get; set; }

        public bool TestimonialStatus { get; set; } 

        public int WriterID { get; set; }

        public virtual Writer Writer { get; set; }  

   
    }
}
