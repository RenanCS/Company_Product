using Bogus;
using Company.Application.InputModel;
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
    public class ProductServiceTests : IClassFixture<ProductServiceFixture>
    {
        private readonly ProductServiceFixture _productServiceFixture;
        private Faker _faker;


        public ProductServiceTests(ProductServiceFixture productServiceFixture)
        {
            _productServiceFixture = productServiceFixture;
            _faker = new Faker();

        }

        [Fact]
        public async Task GetProductsAsync_ShoulReturnList_WhenProductExists()
        {
            IEnumerable<Product> productsStub = FactoryProduct.GetProductsFaker();

            _productServiceFixture
                ._productRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(productsStub);

            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.GetProductsAsync();

            Assert.Equal(productsStub.Count(), result.Count());
        }

        [Fact]
        public async Task GetProductsAsync_ShoulReturnListEmpty_WhenNotProductExists()
        {
            var productsStub = new Product[] { };

            _productServiceFixture
               ._productRepositoryMock
               .Setup(x => x.GetAllAsync())
               .ReturnsAsync(productsStub);

            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.GetProductsAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShoulReturnProduct_WhenIdProductExists()
        {
            Product productsStub = FactoryProduct.GetProductsFaker().FirstOrDefault();

            _productServiceFixture
                ._productRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(productsStub);

            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.GetProductByIdAsync(productsStub.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShoulReturnNull_WhenIdProductNotExists()
        {
            Product productsStub = null;

            _productServiceFixture.
                _productRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(productsStub);

            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.GetProductByIdAsync(_faker.Random.Int(1, 10));

            Assert.Null(result);
        }

        [Fact]
        public async Task AddProductAsync_ShoulReturnTrue_WhenAddNewProduct()
        {
            ProductInputModel productInputModelStub = FactoryProduct.GetProductInputModelFaker(_faker.Random.Int(1, 10));

            _productServiceFixture
                ._productRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Product>()));

            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.AddProductAsync(productInputModelStub);

            Assert.True(result);
        }

        [Fact]
        public async Task AddProductAsync_ShoulReturnFalse_WhenAddProductNull()
        {
            ProductInputModel productInputModelStub = null;

            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.AddProductAsync(productInputModelStub);

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateProductyAsync_ShoulReturnTrue_WhenUpdatedProduct()
        {
            ProductInputModel productInputModelStub = FactoryProduct.GetProductInputModelFaker(_faker.Random.Int(1, 10));

            _productServiceFixture
                ._productRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Product>()));

            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.UpdateProductyAsync(productInputModelStub);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateProductyAsync_ShoulReturnFalse_WhenUpdatedProductNull()
        {
            ProductInputModel productInputModelStub = null;

            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.UpdateProductyAsync(productInputModelStub);

            Assert.False(result);
        }

        [Fact]
        public async Task RemoveProductAsync_ShoulReturnTrue_WhenIdProductExists()
        {
            Product productsStub = FactoryProduct.GetProductsFaker().FirstOrDefault();

            _productServiceFixture._productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productsStub);

            _productServiceFixture._productRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<int>()));

            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.RemoveProductAsync(_faker.Random.Int(1, 10));

            Assert.True(result);
        }

        [Fact]
        public async Task RemoveProductAsync_ShoulReturnFalse_WheIdProductEqualsZero()
        {
            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.RemoveProductAsync(0);

            Assert.False(result);
        }

        [Fact]
        public async Task RemoveProductAsync_ShoulReturnFalse_WheIdProductNotExists()
        {
            Product productsStub = null;

            _productServiceFixture._productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(productsStub);

            _productServiceFixture.MakeSut();

            var result = await _productServiceFixture._productService.RemoveProductAsync(_faker.Random.Int(1, 10));

            Assert.False(result);
        }
    }
}
