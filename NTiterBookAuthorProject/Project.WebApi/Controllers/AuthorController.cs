using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Contracts.Models.RequestModels.Authors;
using Project.Contracts.Models.ResponseModels.Authors;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorManager _authorManager;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAuthorRequestModel> _createAuthorValidator;
        private readonly IValidator<UpdateAuthorRequestModel> _updateAuthorValidator;

        public AuthorController(
            IAuthorManager authorManager,
            IMapper mapper,
            IValidator<CreateAuthorRequestModel> createAuthorValidator,
            IValidator<UpdateAuthorRequestModel> updateAuthorValidator)
        {
            _authorManager = authorManager;
            _mapper = mapper;
            _createAuthorValidator = createAuthorValidator;
            _updateAuthorValidator = updateAuthorValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            List<AuthorDto> authors = await _authorManager.GetAllAsync();
            List<AuthorResponseModel> response = _mapper.Map<List<AuthorResponseModel>>(authors);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            AuthorDto author = await _authorManager.GetByIdAsync(id);
            if (author == null)
                return NotFound("Yazar bulunamadı");

            AuthorResponseModel response = _mapper.Map<AuthorResponseModel>(author);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequestModel model)
        {
            var validation = await _createAuthorValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            AuthorDto dto = _mapper.Map<AuthorDto>(model);
            await _authorManager.CreateAsync(dto);

            return Ok("Author eklendi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorRequestModel model)
        {
            var validation = await _updateAuthorValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            AuthorDto dto = _mapper.Map<AuthorDto>(model);
            dto.Id = id; // id body’den değil, route’dan alınır

            await _authorManager.UpdateAsync(dto);
            return Ok("Author güncellendi");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            string result = await _authorManager.SoftDeleteAsync(id);
            return Ok(result);
        }
    }
}

