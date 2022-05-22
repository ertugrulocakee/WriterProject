using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class HeadingValidator : AbstractValidator<Heading>
    {

        public HeadingValidator()
        {

            RuleFor(x => x.HeadingName).NotEmpty().WithMessage("Başlık adı boş olamaz!");
            RuleFor(x => x.HeadingName).MaximumLength(30).WithMessage("Başlık adı maksimum 30 karakterden oluşabilir!");


        }

    }
}