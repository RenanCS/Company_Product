using AutoMapper;
using Company.Application.InputModel;
using Company.Application.Mappings;
using Company.Application.Service;
using Company.Application.Service.Interface;
using Company.Application.Validators;
using Company.Core.Repositories;
using Company.Infrastructure.Persistence;
using Company.Infrastructure.Persistence.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Company.Infrastructure
{
    public static class ServiceCollectionExtensions
    {


        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }

        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetSection("SqlServer:ConnectionString");

            services.AddDbContext<CompanyDbContext>(
                options => options.UseSqlServer(connection.Value, b => b.MigrationsAssembly("Company.ProducApi")));

            return services;
        }


        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

      

    }
}
