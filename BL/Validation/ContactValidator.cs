using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class ContactValidator : AbstractValidator<Contact>
    {


        public ContactValidator()
        {

            RuleFor(x => x.UserMail).NotEmpty().WithMessage("E-posta boş olamaz!");
            RuleFor(x => x.UserMail).EmailAddress().WithMessage("Lütfen e-posta girişi yapın!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz!");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu başlığı boş olamaz!");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj boş olamaz!");
            RuleFor(x => x.Subject).MinimumLength(5).WithMessage("Konu başlığı en az 5 karakterden oluşmalıdır!");
            RuleFor(x => x.Subject).MaximumLength(30).WithMessage("Konu başlığı maksimum 30 karakterden oluşmalıdır!");
            RuleFor(x => x.Message).MinimumLength(10).WithMessage("Mesaj en az 10 karakterden oluşmalıdır!");
            RuleFor(x => x.Message).MaximumLength(300).WithMessage("Mesaj en fazla 300 karakterden oluşmalıdır!");




        }




    }
}
