using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class AdminValidator : AbstractValidator<Admin>
    {

        public AdminValidator()
        {

            RuleFor(x => x.userName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz!");
            RuleFor(x => x.userName).MaximumLength(40).WithMessage("Kullanıcı adı maksimum 40 karakterden oluşmalıdır!");
            RuleFor(x => x.password).NotEmpty().WithMessage("Şifre boş olamaz!");
            RuleFor(x => x.password).MaximumLength(20).WithMessage("Şifre maksimum 20 karakterden oluşmalıdır!");
            RuleFor(x => x.email).NotEmpty().WithMessage("E-posta boş olamaz!");
            RuleFor(x => x.email).MaximumLength(30).WithMessage("E-posta adresi maksimum 30 karakterden oluşmalıdır!");

        }


    }
}
