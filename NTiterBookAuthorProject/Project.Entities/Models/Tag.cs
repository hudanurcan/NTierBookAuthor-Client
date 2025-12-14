using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<BookTag> BookTags { get; set; }
    }
}
