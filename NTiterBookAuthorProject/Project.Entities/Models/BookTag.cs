using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.Models
{
    public class BookTag : BaseEntity
    {
        public int BookId { get; set; }
        public int TagId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
