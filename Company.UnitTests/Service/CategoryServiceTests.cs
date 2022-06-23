using AutoMapper;
using Bogus;
using Company.Application.Service;
using Company.Application.Service.Interface;
using Company.Core.Entities;
using Company.Core.Repositories;
using Company.UnitTests.Helper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Service
{
    public class CategoryServiceTests
    {
        private ICategoryService _categoryService;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Faker _faker;
        private IMapper _mapper;

        public CategoryServiceTests()
        {
            _faker = new Faker();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapper = AutoMappingHelper.ConfigureAutoMapping();
        }

        #region MAKESUT
        private ICategoryService MakeSut(ICategoryRepository categoryRepositoryStub)
        {
            var stubService = new CategoryService(categoryRepositoryStub, _mapper);

            return stubService;
        }
        #endregion



        [Fact]
        public async Task GetCategoriesAsync_ShoulReturnList_WhenCategoriesExists()
        {
            IEnumerable<Category> categoriesStub = FactoryCategory.GetCategoriesFaker();

            _categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(categoriesStub);

            _categoryService = MakeSut(_categoryRepositoryMock.Object);

            var result = await _categoryService.GetCategoriesAsync();

            Assert.Equal(categoriesStub.Count(), result.Count());
        }

    }
}
