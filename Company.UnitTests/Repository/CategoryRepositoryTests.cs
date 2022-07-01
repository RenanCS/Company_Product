using Company.Core.Repositories;
using Company.Infrastructure.Persistence;
using Company.Infrastructure.Persistence.Repositories;
using Company.UnitTests.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Repository
{
    public class CategoryRepositoryTests
    {
        private ICategoryRepository _categoryRepository;


        #region MAKESUT

        private void MakeSut(DbContextOptions<CompanyDbContext> optionsStub)
        {
            using (var context = new CompanyDbContext(optionsStub))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
            }
        }
        #endregion


        [Fact]
        public async Task GetAllAsync_ShoulReturnListWithFiveCategoies_WhenCategoriesExists()
        {
            var optionsStub = FactoryDbContext.CreateConnection();
            MakeSut(optionsStub);

            using (var context = new CompanyDbContext(optionsStub))
            {
                _categoryRepository = new CategoryRepository(context);

                var result = await _categoryRepository.GetAllAsync();

                Assert.Equal(5, result.Count());
            }
        }

        [Fact]
        public async Task GetAllAsync_ShoulReturnZeroCategory_WhenCategoriesNotExists()
        {
            var optionsStub = FactoryDbContext.CreateConnection();
            MakeSut(optionsStub);

            using (var context = new CompanyDbContext(optionsStub))
            {
                var all = from c in context.Categories select c;
                context.Categories.RemoveRange(all);
                context.SaveChanges();
            }

            using (var context = new CompanyDbContext(optionsStub))
            {
                _categoryRepository = new CategoryRepository(context);

                var result = await _categoryRepository.GetAllAsync();

                Assert.Equal(0, result.Count());
            }

        }


    }
}
