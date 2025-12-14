using FluentValidation;
using Project.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.ModelsValidators
{
    public class BookDtoValidator : BaseDtoValidator<BookDto>
    {
       
            public BookDtoValidator()
            {
                RequiredString(x => x.Title, 2, 150, "Kitap başlığı boş geçilemez");

                RuleFor(x => x.WrittenYear)
                    .InclusiveBetween(1000, DateTime.Now.Year)
                    .WithMessage($"Yazım yılı 1000 ile {DateTime.Now.Year} arasında olmalı");

                RuleFor(x => x.AuthorId)
                    .GreaterThan(0)
                    .WithMessage("AuthorId 0'dan büyük olmalı");

                RuleFor(x => x.CategoryId)
                    .GreaterThan(0)
                    .WithMessage("CategoryId 0'dan büyük olmalı");
            }
        
    }
}
