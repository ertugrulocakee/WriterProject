using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Admin
    {
        [Key]
        public int id { get; set; }

        public string userName { get; set; }
     
        public string password { get; set; }

        public string role { get; set; }

    }
}
