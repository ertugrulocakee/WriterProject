using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class ContentValidator : AbstractValidator<Content>
    {

        public ContentValidator()
        {

            RuleFor(x => x.ContentValue).NotEmpty().WithMessage("İçerik boş olamaz!");
            RuleFor(x => x.ContentValue).MaximumLength(300).WithMessage("İçerik maksimum 300 karakterden oluşabilir!");

        }

    }
}
