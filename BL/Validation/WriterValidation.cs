using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class WriterValidation : AbstractValidator<Writer>
    {

        public WriterValidation()
        {

            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı boş olamaz!");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar soyadı boş olamaz!");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Yazar e-posta adresi boş olamaz!");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Yazar şifresi boş olamaz!");
            RuleFor(x => x.WriterDescription).NotEmpty().WithMessage("Yazar açıklaması boş olamaz!");
            RuleFor(x => x.WriterMail).EmailAddress().WithMessage("Lütfen bir e-posta girişi yapın!");
            RuleFor(x => x.WriterName).MaximumLength(30).WithMessage("Yazar adı maksimum 30 karakterden oluşmalıdır!");
            RuleFor(x => x.WriterSurName).MaximumLength(20).WithMessage("Yazar soyadı maksimum 20 karakterden oluşmalıdır!");
            RuleFor(x => x.WriterMail).MaximumLength(30).WithMessage("Yazar e-posta adresi maksimum 30 karakterden oluşmalıdır!");
            RuleFor(x => x.WriterPassword).MaximumLength(20).WithMessage("Yazar şifresi maksimum 20 karakterden oluşmalıdır!");
            RuleFor(x => x.WriterDescription).MaximumLength(120).WithMessage("Yazar açıklaması maksimum 120 karakterden oluşmalıdır!");

        }


    }
}
