using Project.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.ModelsValidators
{
    public class AuthorDtoValidator : BaseDtoValidator<AuthorDto>
    {
        public AuthorDtoValidator()
        {
            RequiredString(x => x.FirstName, 2, 100, "İsim 2 - 100 karakter arasında olmalıdır");
            RequiredString(x => x.LastName, 5, 200, "Soy İsim 2 - 200 karakter arasında olmalıdır");
        }
    }
}
