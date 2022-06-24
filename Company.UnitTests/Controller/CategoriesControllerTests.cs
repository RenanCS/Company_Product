using Bogus;
using Company.Application.InputModel;
using Company.Application.Service.Interface;
using Company.ProducApi.Controllers;
using Company.UnitTests.Helper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Controller
{
    public class CategoriesControllerTests
    {
        private CategoriesController _controller;
        private Mock<ICategoryService> _categoryServiceMock;
        private Faker _faker;

        public CategoriesControllerTests()
        {
            _faker = new Faker();
            _categoryServiceMock = new Mock<ICategoryService>();
        }


        #region MAKESUT

        private CategoriesController MakeSut(ICategoryService categoryService)
        {
            var controllerStub = new CategoriesController(categoryService);

            return controllerStub;
        }
        #endregion

        [Fact]
        public async Task Get_ShoulReturnList_WhenCategoriesExists()
        {
            IEnumerable<CategoryInputModel> categoriesInputModel = FactoryCategory.GetCategoryInputModel();

            _categoryServiceMock.Setup(x => x.GetCategoriesAsync()).ReturnsAsync(categoriesInputModel);

            _controller = MakeSut(_categoryServiceMock.Object);

            var response = await _controller.Get();
            var result = (OkObjectResult)response.Result;

            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(categoriesInputModel.Count(), ((IEnumerable<CategoryInputModel>)result.Value).Count());
        }

        [Fact]
        public async Task Get_ShoulReturnNotFound_WhenCategoriesNotExists()
        {
            IEnumerable<CategoryInputModel> categoriesInputModel = null;

            _categoryServiceMock.Setup(x => x.GetCategoriesAsync()).ReturnsAsync(categoriesInputModel);

            _controller = MakeSut(_categoryServiceMock.Object);

            var response = await _controller.Get();
            var result = (NotFoundObjectResult)response.Result;

            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
            Assert.Equal("Categories not found", result.Value);
        }
    }
}
