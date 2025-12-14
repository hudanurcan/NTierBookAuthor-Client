using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.Dtos
{
    public class BookTagDto : BaseDto
    {
        public int BookId { get; set; }
        public int TagId { get; set; }
    }
}
