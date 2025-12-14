using FluentValidation;
using Project.Contracts.Models.RequestModels.Authors;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.RequestValidator.Authors
{
    public class CreateAuthorRequestModelValidator : AbstractValidator<CreateAuthorRequestModel>
    {
        public CreateAuthorRequestModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Yazar adı boş geçilemez")
                .Length(2, 50).WithMessage("Yazar adı 2-50 karakter olmalı");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Yazar soyadı boş geçilemez")
                .Length(2, 50).WithMessage("Yazar soyadı 2-50 karakter olmalı");
        }
    }
}
