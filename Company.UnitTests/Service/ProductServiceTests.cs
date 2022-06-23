using AutoMapper;
using Bogus;
using Company.Application.InputModel;
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
    public class ProductServiceTests
    {
        private IProductService _productService;
        private Mock<IProductRepository> _productRepositoryMock;
        private Faker _faker;
        private IMapper _mapper;


        public ProductServiceTests()
        {
            _faker = new Faker();
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapper = AutoMappingHelper.ConfigureAutoMapping();

        }


        #region MAKESUT

        private IProductService MakeSut(IProductRepository stubProductRepository)
        {
            var stubService = new ProductService(stubProductRepository, _mapper);

            return stubService;
        }
        #endregion

        [Fact]
        public async Task GetProductsAsync_ShoulReturnList_WhenProductExists()
        {
            IEnumerable<Product> productsStub = FactoryProduct.GetProductsFaker();

            _productRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(productsStub);

            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.GetProductsAsync();

            Assert.Equal(productsStub.Count(), result.Count());
        }

        [Fact]
        public async Task GetProductsAsync_ShoulReturnListEmpty_WhenNotProductExists()
        {
            var productsStub = new Product[] { };

            _productRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(productsStub);

            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.GetProductsAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShoulReturnProduct_WhenIdProductExists()
        {
            Product productsStub = FactoryProduct.GetProductsFaker().FirstOrDefault();

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productsStub);

            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.GetProductByIdAsync(productsStub.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShoulReturnNull_WhenIdProductNotExists()
        {
            Product productsStub = null;

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productsStub);

            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.GetProductByIdAsync(_faker.Random.Int(1, 10));

            Assert.Null(result);
        }

        [Fact]
        public async Task AddProductAsync_ShoulReturnTrue_WhenAddNewProduct()
        {
            ProductInputModel productInputModelStub = FactoryProduct.GetProductInputModelFaker(_faker.Random.Int(1, 10));

            _productRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Product>()));

            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.AddProductAsync(productInputModelStub);

            Assert.True(result);
        }

        [Fact]
        public async Task AddProductAsync_ShoulReturnFalse_WhenAddProductNull()
        {
            ProductInputModel productInputModelStub = null;

            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.AddProductAsync(productInputModelStub);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateProductyAsync_ShoulReturnTrue_WhenUpdatedProduct()
        {
            ProductInputModel productInputModelStub = FactoryProduct.GetProductInputModelFaker(_faker.Random.Int(1, 10));

            _productRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Product>()));

            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.UpdateProductyAsync(productInputModelStub);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateProductyAsync_ShoulReturnFalse_WhenUpdatedProductNull()
        {
            ProductInputModel productInputModelStub = null;

            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.UpdateProductyAsync(productInputModelStub);

            Assert.False(result);
        }

        [Fact]
        public async Task RemoveProductAsync_ShoulReturnTrue_WhenIdProductExists()
        {
            Product productsStub = FactoryProduct.GetProductsFaker().FirstOrDefault();

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productsStub);

            _productRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<int>()));

            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.RemoveProductAsync(_faker.Random.Int(1, 10));

            Assert.True(result);
        }

        [Fact]
        public async Task RemoveProductAsync_ShoulReturnFalse_WheIdProductEqualsZero()
        {
            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.RemoveProductAsync(0);

            Assert.False(result);
        }

        [Fact]
        public async Task RemoveProductAsync_ShoulReturnFalse_WheIdProductNotExists()
        {
            Product productsStub = null;

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productsStub);

            _productService = MakeSut(_productRepositoryMock.Object);

            var result = await _productService.RemoveProductAsync(_faker.Random.Int(1,10));

            Assert.False(result);
        }
    }
}
