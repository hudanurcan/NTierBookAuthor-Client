using Project.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contracts.Models.ResponseModels.Books
{
    public class BookResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int WrittenYear { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public DataStatus Status { get; set; }

    }
}
