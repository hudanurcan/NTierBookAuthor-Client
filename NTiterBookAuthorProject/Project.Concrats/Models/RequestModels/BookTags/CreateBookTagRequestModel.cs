using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contracts.Models.RequestModels.BookTags
{
    public class CreateBookTagRequestModel
    {
        public int BookId { get; set; }
        public int TagId { get; set; }
    }
}
