using Company.Core.Entities;
using Company.UnitTests.Fixtures;
using Company.UnitTests.Helper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Service
{
    public class CategoryServiceTests : IClassFixture<CategoryServiceFixture>
    {
        private readonly CategoryServiceFixture _categoryServiceFixture;

        public CategoryServiceTests(CategoryServiceFixture categoryServiceFixture)
        {
            _categoryServiceFixture = categoryServiceFixture;
        }

        [Fact]
        public async Task GetCategoriesAsync_ShoulReturnList_WhenCategoriesExists()
        {
            IEnumerable<Category> categoriesStub = FactoryCategory.GetCategoriesFaker();

            _categoryServiceFixture._categoryRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(categoriesStub);

            _categoryServiceFixture.MakeSut();

            var result = await _categoryServiceFixture._categoryService.GetCategoriesAsync();

            Assert.Equal(categoriesStub.Count(), result.Count());
        }

    }
}
