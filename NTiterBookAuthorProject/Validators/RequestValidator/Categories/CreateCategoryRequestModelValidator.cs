using FluentValidation;
using Project.Contracts.Models.RequestModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.RequestValidator.Categories
{
    public class CreateCategoryRequestModelValidator : AbstractValidator<CreateCategoryRequestModel>
    {
        public CreateCategoryRequestModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş olamaz")
                .Length(3, 50).WithMessage("Kategori adı 3 - 50 karakter arasında olmalı");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş geçilemez")
                .Length(5, 200).WithMessage("Açıklama adı 5 - 200 karakter arasında olmalı");
        }
    }
}
