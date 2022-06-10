using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class SkillValidator : AbstractValidator<Skill>
    {

        public SkillValidator()
        {


            RuleFor(x => x.SkillName).NotEmpty().WithMessage("Yetenek adı boş olamaz!");
            RuleFor(x => x.SkillName).MaximumLength(50).WithMessage("Yetenek adı maksimum 50 karakterden oluşabilir!");
            RuleFor(x => x.SkillOverall).NotEmpty().WithMessage("Yetenek bilgisi boş olamaz!");
            RuleFor(x => x.SkillOverall).LessThan(101).WithMessage("Yetenek bilgisi 100 değerinden büyük olamaz!");
            RuleFor(x => x.SkillOverall).GreaterThan(0).WithMessage("Yetenek bilgisi negatif değere sahip olamaz! 0 olamaz!");
         


        }


    }
}
