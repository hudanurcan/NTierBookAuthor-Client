using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Contracts.Models.RequestModels.Books;
using Project.Contracts.Models.ResponseModels.Books;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager _bookManager;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookRequestModel> _createBookValidator;
        private readonly IValidator<UpdateBookRequestModel> _updateBookValidator;

        public BookController(
            IBookManager bookManager,
            IMapper mapper,
            IValidator<CreateBookRequestModel> createBookValidator,
            IValidator<UpdateBookRequestModel> updateBookValidator)
        {
            _bookManager = bookManager;
            _mapper = mapper;
            _createBookValidator = createBookValidator;
            _updateBookValidator = updateBookValidator;
        }

      
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            List<BookDto> books = await _bookManager.GetAllAsync();
            List<BookResponseModel> response = _mapper.Map<List<BookResponseModel>>(books);
            return Ok(response);
        }

     
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            BookDto book = await _bookManager.GetByIdAsync(id);
            if (book == null)
                return NotFound("Kitap bulunamadı");

            BookResponseModel response = _mapper.Map<BookResponseModel>(book);
            return Ok(response);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookRequestModel model)
        {
            var validation = await _createBookValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            BookDto dto = _mapper.Map<BookDto>(model);
            await _bookManager.CreateAsync(dto);

            return Ok("Book eklendi");
        }

     
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookRequestModel model)
        {
            var validation = await _updateBookValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            BookDto dto = _mapper.Map<BookDto>(model);
            dto.Id = id; // id body’den değil route’dan gelir

            await _bookManager.UpdateAsync(dto);
            return Ok("Book güncellendi");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            string result = await _bookManager.SoftDeleteAsync(id);
            return Ok(result);
        }
    }
}

