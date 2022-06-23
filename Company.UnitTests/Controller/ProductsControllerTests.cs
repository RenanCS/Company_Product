using AutoFixture;
using Bogus;
using Company.Application.InputModel;
using Company.Application.Service.Interface;
using Company.Application.Validators;
using Company.ProducApi.Controllers;
using Company.UnitTests.Helper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Controller
{
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private Mock<IProductService> _productServiceMock;
        private Faker _faker;

        public ProductsControllerTests()
        {
            _faker = new Faker();
            _productServiceMock = new Mock<IProductService>();
        }


        #region MAKESUT

        private ProductsController MakeSut(IProductService productServiceStub)
        {
            var controllerStub = new ProductsController(productServiceStub);

            return controllerStub;
        }
        #endregion

        [Fact]
        public async Task Get_ShoulReturnList_WhenProductExists()
        {
            IEnumerable<ProductInputModel> productsInputModel = FactoryProduct.GetProductsInputModelFaker();

            _productServiceMock.Setup(x => x.GetProductsAsync()).ReturnsAsync(productsInputModel);

            _controller = MakeSut(_productServiceMock.Object);

            var response = await _controller.Get();
            var result = (OkObjectResult)response.Result;

            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(productsInputModel.Count(), ((IEnumerable<ProductInputModel>)result.Value).Count());
        }

        [Fact]
        public async Task Get_ShoulReturnNotFound_WhenProductsNotExists()
        {
            IEnumerable<ProductInputModel> productsInputModel = null;

            _productServiceMock.Setup(x => x.GetProductsAsync()).ReturnsAsync(productsInputModel);

            _controller = MakeSut(_productServiceMock.Object);

            var response = await _controller.Get();
            var result = (NotFoundObjectResult)response.Result;

            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
            Assert.Equal("products not found", result.Value);
        }

        [Fact]
        public async Task GetProduct_ShoulReturnProductInputModel_WhenProductsExists()
        {
            var idProduct = _faker.Random.Int(1, 10);
            ProductInputModel productInputModel = FactoryProduct.GetProductInputModelFaker(idProduct);

            _productServiceMock.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(productInputModel);

            _controller = MakeSut(_productServiceMock.Object);

            var response = await _controller.Get(idProduct);
            var result = (OkObjectResult)response.Result;

            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(productInputModel, result.Value);
        }

        [Fact]
        public async Task GetProduct_ShoulReturnNotFound_WhenProductsNotExists()
        {
            var idProduct = _faker.Random.Int(1, 10);
            ProductInputModel productInputModel = null;

            _productServiceMock.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(productInputModel);

            _controller = MakeSut(_productServiceMock.Object);

            var response = await _controller.Get(idProduct);
            var result = (NotFoundObjectResult)response.Result;

            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
            Assert.Equal("Product not found", result.Value);
        }

        [Fact]
        public async Task Post_ShoulReturnProduct_WhenAddNewProduct()
        {
            ProductInputModel productInputModel = FactoryProduct.GetProductInputModelFaker(0);

            _productServiceMock.Setup(x => x.AddProductAsync(It.IsAny<ProductInputModel>()));

            _controller = MakeSut(_productServiceMock.Object);

            var response = await _controller.Post(productInputModel);
            var result = (CreatedAtRouteResult)response;

            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            Assert.Equal(productInputModel, result.Value);
        }

        [Fact]
        public async Task Post_ShoulReturnBadRequest_WhenAddNewProductNull()
        {
            ProductInputModel productInputModel = null;
                    
            _controller = MakeSut(_productServiceMock.Object);

            var response = await _controller.Post(productInputModel);
            var result = (BadRequestObjectResult)response;

            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            Assert.Equal("Invalid Data", result.Value);
        }

        [Fact]
        public async Task Post_ShoulReturnValidation_WhenAddNewProductPartiallyComplete()
        {
            ProductInputModel productInputModel = FactoryProduct.GetProductInputModelFaker(0);
            productInputModel.Name = null;
                              
            _controller = MakeSut(_productServiceMock.Object);
            _controller.ModelState.AddModelError("Name", "Nome do produto dever ser válido");

            var response = await _controller.Post(productInputModel);
            var result = (BadRequestObjectResult)response;

            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);

            var serializableError = Assert.IsType<SerializableError>(result.Value);
            Assert.True(((string[])serializableError["Name"])[0] == "Nome do produto dever ser válido");

        }

        [Fact]
        public async Task Put_ShoulReturnProduct_WhenUpdatedProduct()
        {
            ProductInputModel productInputModel = FactoryProduct.GetProductInputModelFaker(0);

            _productServiceMock.Setup(x => x.UpdateProductyAsync(It.IsAny<ProductInputModel>()));

            _controller = MakeSut(_productServiceMock.Object);

            var response = await _controller.Put(productInputModel);
            var result = (CreatedAtRouteResult)response;

            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
            Assert.Equal(productInputModel, result.Value);
        }

        [Fact]
        public async Task Puut_ShoulReturnBadRequest_WhenProductIsNull()
        {
            ProductInputModel productInputModel = null;
                                 
            _controller = MakeSut(_productServiceMock.Object);

            var response = await _controller.Post(productInputModel);
            var result = (BadRequestObjectResult)response;

            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            Assert.Equal("Invalid Data", result.Value);
        }

        [Fact]
        public async Task Delete_ShoulReturnProductInputModel_WhenRemoveProductExists()
        {
            var idProduct = _faker.Random.Int(1, 10);
            ProductInputModel productInputModel = FactoryProduct.GetProductInputModelFaker(idProduct);

            _productServiceMock.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(productInputModel);
            _productServiceMock.Setup(x => x.RemoveProductAsync(It.IsAny<int>()));

            _controller = MakeSut(_productServiceMock.Object);

            var response = await _controller.Delete(idProduct);
            var result = (ObjectResult)response;

            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.Equal(productInputModel, result.Value);
        }

        [Fact]
        public async Task Delete_ShoulReturnNotFound_WhenNotProductExists()
        {
            var idProduct = _faker.Random.Int(1, 10);
            ProductInputModel productInputModel = null;

            _productServiceMock.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(productInputModel);

            _controller = MakeSut(_productServiceMock.Object);

            var response = await _controller.Delete(idProduct);
            var result = (NotFoundObjectResult)response;

            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
            Assert.Equal("Product not found", result.Value);
        }


    }
}