using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Contracts.Models.RequestModels.BookTags;
using Project.Contracts.Models.ResponseModels.BookTags;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTagController : ControllerBase
    {
        private readonly IBookTagManager _bookTagManager;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookTagRequestModel> _createValidator;

        public BookTagController(
            IBookTagManager bookTagManager,
            IMapper mapper,
            IValidator<CreateBookTagRequestModel> createValidator)
        {
            _bookTagManager = bookTagManager;
            _mapper = mapper;
            _createValidator = createValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookTags()
        {
            List<BookTagDto> list = await _bookTagManager.GetAllAsync();
            List<BookTagResponseModel> response = _mapper.Map<List<BookTagResponseModel>>(list);
            return Ok(response);
        }

        [HttpGet("book/{bookId}/tag/{tagId}")]
        public async Task<IActionResult> GetBookTag(int bookId, int tagId)
        {
            // BaseManager GetByIdAsync int çalışmaz çünkü join tabloda composite key var.
            // Bu yüzden Manager’da özel bir metot olmalıdır.
            BookTagDto dto = await _bookTagManager.GetByBookAndTagIdAsync(bookId, tagId);

            if (dto == null)
                return NotFound("BookTag ilişkisi bulunamadı");

            BookTagResponseModel response = _mapper.Map<BookTagResponseModel>(dto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookTag([FromBody] CreateBookTagRequestModel model)
        {
            var validation = await _createValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            BookTagDto dto = _mapper.Map<BookTagDto>(model);
            await _bookTagManager.CreateAsync(dto);

            return Ok("Book-Tag ilişkisi eklendi");
        }

        [HttpDelete("book/{bookId}/tag/{tagId}")]
        public async Task<IActionResult> DeleteBookTag(int bookId, int tagId) // soft delete
        {

            // Manager’da özel delete metodu olmalı.
            string result = await _bookTagManager.DeleteByBookAndTagIdAsync(bookId, tagId);
            return Ok(result);
        }
    }
}
