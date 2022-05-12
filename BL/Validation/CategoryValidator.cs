using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validation
{
    public class CategoryValidator : AbstractValidator<Category>
    {

        public CategoryValidator()
        {

            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori Adını boş bırakamazsın!");
            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("Açıklamayı boş bırakamazsın!");
            RuleFor(x => x.CategoryName).MinimumLength(3).WithMessage("Kategori adı minimum 3 karakterden oluşmalıdır!");
            RuleFor(x => x.CategoryName).MaximumLength(20).WithMessage("Kategori adı maksimum 20 karakterden oluşmalıdır!");
            RuleFor(x => x.CategoryDescription).MinimumLength(5).WithMessage("Açıklama minimum 5 karakterden oluşmalıdır!");
            RuleFor(x => x.CategoryDescription).MaximumLength(200).WithMessage("Açıklama maksimum 200 karakterden oluşmalıdır!");


        }



    }
}
