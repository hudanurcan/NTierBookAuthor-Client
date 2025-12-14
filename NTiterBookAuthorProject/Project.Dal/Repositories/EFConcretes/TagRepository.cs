using Project.Dal.ContextClasses;
using Project.Dal.Repositories.Abstracts;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Repositories.EFConcretes
{
    public class TagRepository(MyContext context) : BaseRepository<Tag>(context), ITagRepository
    {
    }
}
