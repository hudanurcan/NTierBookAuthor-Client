using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Bll.Dtos;
using Project.Bll.Managers.Abstracts;
using Project.Contracts.Models.RequestModels.Categories;
using Project.Contracts.Models.ResponseModels.Categories;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryRequestModel> _createValidator;
        private readonly IValidator<UpdateCategoryRequestModel> _updateValidator;

        public CategoryController(
            ICategoryManager categoryManager,
            IMapper mapper,
            IValidator<CreateCategoryRequestModel> createValidator,
            IValidator<UpdateCategoryRequestModel> updateValidator)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            List<CategoryDto> categories = await _categoryManager.GetAllAsync();
            List<CategoryResponseModel> response =
                _mapper.Map<List<CategoryResponseModel>>(categories);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            CategoryDto category = await _categoryManager.GetByIdAsync(id);
            if (category == null)
                return NotFound("Kategori bulunamadı");

            CategoryResponseModel response =
                _mapper.Map<CategoryResponseModel>(category);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestModel model)
        {
            var validation = await _createValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            CategoryDto dto = _mapper.Map<CategoryDto>(model);
            await _categoryManager.CreateAsync(dto);

            return Ok("Kategori eklendi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequestModel model)
        {
            var validation = await _updateValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            CategoryDto dto = _mapper.Map<CategoryDto>(model);
            dto.Id = id;  // id route’dan gelir

            await _categoryManager.UpdateAsync(dto);
            return Ok("Kategori güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id) // softdelete
        {
            string result = await _categoryManager.SoftDeleteAsync(id);
            return Ok(result);
        }
    }
}

