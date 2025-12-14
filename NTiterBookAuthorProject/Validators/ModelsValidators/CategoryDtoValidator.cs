using Project.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.ModelsValidators
{
    public class CategoryDtoValidator : BaseDtoValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {

            RequiredString(x => x.Name, 2, 200, "Kategori adı 2 - 200 karakter arasında olmalıdır");
            RequiredString(x => x.Description, 2, 500, "Açıklama 2 - 500 karakter arasında olmalıdır");

        }
    }
}
