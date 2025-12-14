using FluentValidation;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.ModelsValidators
{
    public class BookTagDtoValidator : BaseDtoValidator<BookTag>
    {
        public BookTagDtoValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0)
                .WithMessage("BookId 0'dan büyük olmalı");

            RuleFor(x => x.TagId)
                .GreaterThan(0)
                .WithMessage("TagId 0'dan büyük olmalı");
        }
    }
}
