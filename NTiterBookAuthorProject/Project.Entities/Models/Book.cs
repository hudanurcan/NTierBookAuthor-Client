using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models
{
    public class Book : BaseEntity
    {
        public Book()
        {
            BookTags = new HashSet<BookTag>();
        }

        public string Title { get; set; }
        public int WrittenYear { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<BookTag> BookTags { get; set; }

    }
}
