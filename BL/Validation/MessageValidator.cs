using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class MessageValidator : AbstractValidator<Message>
    {

        public MessageValidator()
        {

            //RuleFor(x => x.SenderMail).NotEmpty().WithMessage("Gönderen e-postası boş olamaz!");
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı e-postası boş olamaz!");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Mesaj başlığı boş olamaz!");
            RuleFor(x => x.MessageDescription).NotEmpty().WithMessage("Mesaj içeriği boş olamaz!");
            RuleFor(x => x.Title).MaximumLength(30).WithMessage("Mesaj başlığı maksimum 30 karakterden oluşabilir!");
            RuleFor(x => x.MessageDescription).MaximumLength(300).WithMessage("Mesaj içeriği maksimum 300 karakterden oluşabilir!");
            


        }


    }
}
