using FluentValidation;
using Project.Contracts.Models.RequestModels.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.RequestValidator.Authors
{
    public class UpdateAuthorRequestModelValidator : AbstractValidator<UpdateAuthorRequestModel>
    {
        public UpdateAuthorRequestModelValidator()
        {
            //RuleFor(x => x.Id)
            //    .GreaterThan(0).WithMessage("Id 0'dan büyük olmalı");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Yazar adı boş geçilemez")
                .Length(2, 50).WithMessage("Yazar adı 2-50 karakter olmalı");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Yazar soyadı boş geçilemez")
                .Length(2, 50).WithMessage("Yazar soyadı 2-50 karakter olmalı");
        }
    }
}
