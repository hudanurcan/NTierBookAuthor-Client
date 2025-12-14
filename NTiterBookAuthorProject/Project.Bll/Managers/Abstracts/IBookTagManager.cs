using Project.Bll.Dtos;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.Managers.Abstracts
{
    public interface IBookTagManager : IManager<BookTagDto, BookTag>
    {
        Task<BookTagDto> GetByBookAndTagIdAsync(int bookId, int tagId);
        Task<string> DeleteByBookAndTagIdAsync(int bookId, int tagId);
    }
}
