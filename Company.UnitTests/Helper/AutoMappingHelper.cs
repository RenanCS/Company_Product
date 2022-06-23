using AutoMapper;
using Company.Application.Mappings;

namespace Company.UnitTests.Helper
{
    public static class AutoMappingHelper
    {
        public static IMapper ConfigureAutoMapping()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}
