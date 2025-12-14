using AutoMapper;
using Project.Bll.Dtos;
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
    public class TagManager(ITagRepository repository, IMapper mapper, IManagerExecutor executor) : BaseManager<TagDto, Tag>(repository, mapper, executor), ITagManager
    {
        private readonly ITagRepository _repository = repository;
    }
}
