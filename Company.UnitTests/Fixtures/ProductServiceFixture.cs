using AutoMapper;
using Company.Application.Service;
using Company.Application.Service.Interface;
using Company.Core.Repositories;
using Company.UnitTests.Helper;
using Moq;

namespace Company.UnitTests.Fixtures
{
    public class ProductServiceFixture
    {
        public IProductService _productService { get; private set; }
        public Mock<IProductRepository> _productRepositoryMock { get; }
        private IMapper _mapper;

        public ProductServiceFixture()
        {
            _mapper = AutoMappingHelper.ConfigureAutoMapping();
            _productRepositoryMock = new Mock<IProductRepository>();
        }

        public void MakeSut()
        {
            _productService = new ProductService(_productRepositoryMock.Object, _mapper);
        }
    }
}
