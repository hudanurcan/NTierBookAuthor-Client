using FluentValidation;
using Project.Contracts.Models.RequestModels.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.RequestValidator.Books
{
    public class CreateBookRequestModelValidator  : AbstractValidator<CreateBookRequestModel>
    {
        public CreateBookRequestModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Kitap başlığı boş geçilemez")
                .Length(2, 150).WithMessage("Başlık 2-150 karakter olmalı");

            RuleFor(x => x.WrittenYear)
                .InclusiveBetween(1500, DateTime.Now.Year)
                .WithMessage($"Yazım yılı 1500 ile {DateTime.Now.Year} arasında olmalı");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("AuthorId 0'dan büyük olmalı");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId 0'dan büyük olmalı");
        }
    }
}
