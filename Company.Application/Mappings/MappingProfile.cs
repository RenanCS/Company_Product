using AutoMapper;
using Company.Application.InputModel;
using Company.Core.Entities;

namespace Company.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryInputModel>().ReverseMap();

            CreateMap<ProductInputModel, Product>();

            CreateMap<Product, ProductInputModel>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
