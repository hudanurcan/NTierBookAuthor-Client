using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Bll.Dtos;
using Project.Bll.Exceptions.BusinessException;
using Project.Bll.Executors.Abstract;
using Project.Bll.Managers.Abstracts;
using Project.Dal.Repositories.Abstracts;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Project.Bll.Managers.Concretes
{
    public class BookTagManager(IBookTagRepository repository, IMapper mapper, IManagerExecutor executor) : BaseManager<BookTagDto, BookTag>(repository, mapper, executor), IBookTagManager
    {
        private readonly IBookTagRepository _repository = repository;
        private readonly IManagerExecutor _executor = executor;
        private readonly IMapper _mapper = mapper;
        protected override async Task CreateCoreAsync(BookTagDto entity)
        {
            bool exists = _repository
                .Where(x => x.BookId == entity.BookId && x.TagId == entity.TagId)
                .Any();

            if (exists)
                throw new BusinessException("Bu kitap-tag ilişkisi zaten var.");

            await base.CreateCoreAsync(entity);
        }
        public async Task<BookTagDto> GetByBookAndTagIdAsync(int bookId, int tagId)
        {
            return await _executor.ExecuteAsync(async () =>
            {
                var entity = await _repository
                    .Where(x => x.BookId == bookId && x.TagId == tagId)
                    .FirstOrDefaultAsync();

                return _mapper.Map<BookTagDto>(entity);
            });
        }

        public async Task<string> DeleteByBookAndTagIdAsync(int bookId, int tagId)
        {
            return await _executor.ExecuteAsync(async () =>
            {
                var entity = await _repository
                    .Where(x => x.BookId == bookId && x.TagId == tagId)
                    .FirstOrDefaultAsync();

                if (entity == null)
                    throw new BusinessException("BookTag ilişkisi bulunamadı");

                // Soft delete mantığın varsa:
                entity.Status = Project.Entities.Enums.DataStatus.Deleted;
                entity.DeletedDate = DateTime.Now;
                await _repository.SaveChangesAsync();

                return "Book-Tag ilişkisi silindi";
            });
        }
    }
}
