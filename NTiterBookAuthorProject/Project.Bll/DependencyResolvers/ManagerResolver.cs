using Microsoft.Extensions.DependencyInjection;
using Project.Bll.Managers.Abstracts;
using Project.Bll.Managers.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Bll.Executors;
using Project.Bll.Executors.Abstract;
using Project.Bll.Executors.Concrete;

namespace Project.Bll.DependencyResolvers
{
    public static class ManagerResolver
    {
        public static void AddManagerService(this IServiceCollection services)
        {
            services.AddScoped<IAuthorManager, AuthorManager>();
            services.AddScoped<IBookTagManager, BookTagManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<ITagManager, TagManager>();
            services.AddScoped<IManagerExecutor, ManagerExecutor>();

        }
    }
}
