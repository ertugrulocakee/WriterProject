using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ImageFile
    {

        [Key]
        public int imageID { get; set; }

        public string imageName { get; set; }


        public string imagePath { get; set; }


    }
}
