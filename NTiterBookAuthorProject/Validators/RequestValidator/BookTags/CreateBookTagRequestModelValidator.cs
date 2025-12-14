using FluentValidation;
using Project.Contracts.Models.RequestModels.BookTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.RequestValidator.BookTags
{
    public class CreateBookTagRequestModelValidator : AbstractValidator<CreateBookTagRequestModel>
    {
        public CreateBookTagRequestModelValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("BookId 0'dan büyük olmalı");

            RuleFor(x => x.TagId)
                .GreaterThan(0).WithMessage("TagId 0'dan büyük olmalı");
        }
    }
}
