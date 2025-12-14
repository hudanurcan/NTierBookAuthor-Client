using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contracts.Models.RequestModels.Books
{
    public class UpdateBookRequestModel
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        public int WrittenYear { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
    }
}
