using AutoMapper;
using Company.Application.InputModel;
using Company.Application.Service.Interface;
using Company.Core.Entities;
using Company.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddProductAsync(ProductInputModel productInputModel)
        {
            var product = _mapper.Map<Product>(productInputModel);
            await _productRepository.CreateAsync(product);
            productInputModel.Id = product.Id;
        }

        public async Task UpdateProductyAsync(ProductInputModel productInputModel)
        {
            var product = _mapper.Map<Product>(productInputModel);
            await _productRepository.UpdateAsync(product);
        }

        public async Task RemoveProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product.Id);
        }

        public async Task<ProductInputModel> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductInputModel>(product);
        }

        public async Task<IEnumerable<ProductInputModel>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductInputModel>>(products);
        }



    }
}
