using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }

        public string SkillName { get; set; }

        public int SkillOverall { get; set; }

        public int WriterID { get; set; }
        public virtual Writer Writer { get; set; }

        public bool SkillStatus { get; set; }

    }
}
