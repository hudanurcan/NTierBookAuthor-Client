using FluentValidation;
using Project.Contracts.Models.ResponseModels.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.ResponseValidator.Authors
{
    public class AuthorResponseModelValidator: AbstractValidator<AuthorResponseModel>
    {
        public AuthorResponseModelValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
