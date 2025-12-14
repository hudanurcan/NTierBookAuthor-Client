using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Contracts.Models.RequestModels.Tags;
using Project.Contracts.Models.ResponseModels.Tags;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagManager _tagManager;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTagRequestModel> _createValidator;
        private readonly IValidator<UpdateTagRequestModel> _updateValidator;

        public TagController(
            ITagManager tagManager,
            IMapper mapper,
            IValidator<CreateTagRequestModel> createValidator,
            IValidator<UpdateTagRequestModel> updateValidator)
        {
            _tagManager = tagManager;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            List<TagDto> tags = await _tagManager.GetAllAsync();
            List<TagResponseModel> response = _mapper.Map<List<TagResponseModel>>(tags);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            TagDto tag = await _tagManager.GetByIdAsync(id);
            if (tag == null)
                return NotFound("Tag bulunamadı");

            TagResponseModel response = _mapper.Map<TagResponseModel>(tag);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagRequestModel model)
        {
            var validation = await _createValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            TagDto dto = _mapper.Map<TagDto>(model);
            await _tagManager.CreateAsync(dto);

            return Ok("Tag eklendi");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] UpdateTagRequestModel model)
        {
            var validation = await _updateValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            TagDto dto = _mapper.Map<TagDto>(model);
            dto.Id = id; // id sadece route'tan alınır

            await _tagManager.UpdateAsync(dto);
            return Ok("Tag güncellendi");
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id) // soft delete
        {
            string result = await _tagManager.SoftDeleteAsync(id);
            return Ok(result);
        }
    }
}

