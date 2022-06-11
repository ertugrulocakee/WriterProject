using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class TestimonialValidator : AbstractValidator<Testimonial>
    {

        public TestimonialValidator()
        {

            RuleFor(x => x.TestimonialValue).NotEmpty().WithMessage("Referansınız boş olamaz!");
            RuleFor(x => x.TestimonialValue).MaximumLength(150).WithMessage("Referansınız 150 karakterden fazla karakter içeremez!");

        }


    }
}
