using AutoMapper;
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

namespace Project.Bll.Managers.Concretes
{
    //public class BookManager(IBookRepository repository, IMapper mapper, IManagerExecutor executor) : BaseManager<BookDto, Book>(repository, mapper, executor), IBookManager
    //{
    //    private readonly IBookRepository _repository = repository;
    //}
    public class BookManager : BaseManager<BookDto, Book>, IBookManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BookManager(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IManagerExecutor executor)
            : base(bookRepository, mapper, executor)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
        }

        // CREATE için business kontrol
        protected override async Task CreateCoreAsync(BookDto entity)
        {
            // Author var mı?
            var author = await _authorRepository.GetByIdAsync(entity.AuthorId);
            if (author == null)
                throw new BusinessException("Böyle bir Author yok.");

            // Category var mı?
            var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);
            if (category == null)
                throw new BusinessException("Böyle bir Category yok.");

            // her şey tamam -> base create çalışsın
            await base.CreateCoreAsync(entity);
        }

        // UPDATE için business kontrol
        protected override async Task UpdateCoreAsync(BookDto entity)
        {
            // Author var mı?
            var author = await _authorRepository.GetByIdAsync(entity.AuthorId);
            if (author == null)
                throw new BusinessException("Güncellemede verilen AuthorId bulunamadı.");

            // Category var mı?
            var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);
            if (category == null)
                throw new BusinessException("Güncellemede verilen CategoryId bulunamadı.");

            await base.UpdateCoreAsync(entity);
        }
    }
}
