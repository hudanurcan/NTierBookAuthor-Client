using FluentValidation;
using Project.Contracts.Models.ResponseModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.ResponseValidator.Categories
{
    public class CategoryResponseModelValidator : AbstractValidator<CategoryResponseModel>
    {
        public CategoryResponseModelValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
