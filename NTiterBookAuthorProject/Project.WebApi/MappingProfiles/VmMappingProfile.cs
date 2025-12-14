using AutoMapper;
using Project.Bll.Dtos;
using Project.Contracts.Models.RequestModels.Authors;
using Project.Contracts.Models.RequestModels.Books;
using Project.Contracts.Models.RequestModels.BookTags;
using Project.Contracts.Models.RequestModels.Categories;
using Project.Contracts.Models.RequestModels.Tags;
using Project.Contracts.Models.ResponseModels.Authors;
using Project.Contracts.Models.ResponseModels.Books;
using Project.Contracts.Models.ResponseModels.BookTags;
using Project.Contracts.Models.ResponseModels.Categories;
using Project.Contracts.Models.ResponseModels.Tags;



namespace Project.WebApi.MappingProfiles
{
    public class VmMappingProfile : Profile
    {
        public VmMappingProfile()
        {
            CreateMap<CreateCategoryRequestModel, CategoryDto>();
            CreateMap<UpdateCategoryRequestModel, CategoryDto>();
            CreateMap<CategoryDto, CategoryResponseModel>();

            CreateMap<CreateAuthorRequestModel, AuthorDto>();
            CreateMap<UpdateAuthorRequestModel, AuthorDto>();
            CreateMap<AuthorDto, AuthorResponseModel>();

            CreateMap<CreateBookTagRequestModel, BookTagDto>();
            CreateMap<UpdateBookTagRequestModel, BookTagDto>();
            CreateMap<BookTagDto, BookTagResponseModel>();

            CreateMap<CreateBookRequestModel, BookDto>();
            CreateMap<UpdateBookRequestModel, BookDto>();
            CreateMap<BookDto, BookResponseModel>();

            CreateMap<CreateTagRequestModel, TagDto>();
            CreateMap<UpdateTagRequestModel, TagDto>();
            CreateMap<TagDto, TagResponseModel>();
        }
    }
}
