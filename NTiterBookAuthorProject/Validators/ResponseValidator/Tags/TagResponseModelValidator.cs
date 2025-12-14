using FluentValidation;
using Project.Contracts.Models.ResponseModels.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.ResponseValidator.Tags
{
    public class BookResponseModelValidator  : AbstractValidator<BookResponseModel>
    {
        public BookResponseModelValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.WrittenYear).GreaterThan(0);
            RuleFor(x => x.AuthorId).GreaterThan(0);
            RuleFor(x => x.CategoryId).GreaterThan(0);
        }
    }
}
