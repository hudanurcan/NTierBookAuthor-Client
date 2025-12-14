using FluentValidation;
using Project.Contracts.Models.RequestModels.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.RequestValidator.Tags
{
    public class CreateTagRequestModelValidator : AbstractValidator<CreateTagRequestModel>
    {
        public CreateTagRequestModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Etiket adı boş geçilemez")
                .Length(2, 50).WithMessage("Etiket adı 2-50 karakter olmalı");
        }
    }
}
