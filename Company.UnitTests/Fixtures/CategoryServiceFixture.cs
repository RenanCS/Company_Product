using AutoMapper;
using Company.Application.Service;
using Company.Application.Service.Interface;
using Company.Core.Repositories;
using Company.UnitTests.Helper;
using Moq;

namespace Company.UnitTests.Fixtures
{
    public class CategoryServiceFixture
    {
        public ICategoryService _categoryService { get; private set; }
        public Mock<ICategoryRepository> _categoryRepositoryMock { get; }
        private IMapper _mapper;

        public CategoryServiceFixture()
        {
            _mapper = AutoMappingHelper.ConfigureAutoMapping();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
        }

        public void MakeSut()
        {
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapper);
        }
    }
}
