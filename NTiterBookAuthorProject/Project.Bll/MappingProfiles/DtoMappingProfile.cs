using AutoMapper;
using Project.Bll.Dtos;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.MappingProfiles
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Author,AuthorDto>().ReverseMap();
            CreateMap<BookTag,BookTagDto>().ReverseMap();
            CreateMap<Tag,TagDto>().ReverseMap();
            CreateMap<Book,BookDto>().ReverseMap();
        }
    }
}
