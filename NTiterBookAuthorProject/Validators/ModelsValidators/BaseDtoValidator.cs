using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Validators.ModelsValidators
{

    public abstract class BaseDtoValidator<T> : AbstractValidator<T>
    {
        protected void RequiredString( // ortak validation kuralları
              System.Linq.Expressions.Expression<Func<T, string>> exp, // Expression<Func<T, string>> exp : x => x.UserName ifadesidir.
                                                                       // <T, string> : T tipinde verilecek, o da içinden string  olan bir alan (ör UserName) seçecek
              int min,
              int max,
              string message)
        {
            RuleFor(exp) // kuralı başlatır. exp hangi alan ise o alan için kural tanımlar.
                .NotEmpty().WithMessage(message) // kural ihlali olursa message gösterilir
                .Length(min, max).WithMessage($"{message} ({min}-{max} karakter)"); // eksik karakter kuralı mesajı
        }
    }
}
