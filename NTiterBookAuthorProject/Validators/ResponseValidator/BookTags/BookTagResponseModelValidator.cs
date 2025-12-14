using FluentValidation;
using Project.Contracts.Models.ResponseModels.BookTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.ResponseValidator.BookTags
{
    public class BookTagResponseModelValidator
        : AbstractValidator<BookTagResponseModel>
    {
        public BookTagResponseModelValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
            RuleFor(x => x.TagId).GreaterThan(0);
        }
    }
}
