using Project.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.ModelsValidators
{
    public class TagDtoValidator : BaseDtoValidator<TagDto>
    {
        public TagDtoValidator()
        {
            RequiredString(x => x.Name, 2, 50, "Etiket adı boş geçilemez");
        }
    }
}
