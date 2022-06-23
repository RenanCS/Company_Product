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

        public async Task<bool> AddProductAsync(ProductInputModel productInputModel)
        {
            if(productInputModel == null)
            {
                return false;
            }

            var product = _mapper.Map<Product>(productInputModel);
            await _productRepository.CreateAsync(product);
            productInputModel.Id = product.Id;
            return true;
        }

        public async Task<bool> UpdateProductyAsync(ProductInputModel productInputModel)
        {
            if(productInputModel == null)
            {
                return false;
            }

            var product = _mapper.Map<Product>(productInputModel);
            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> RemoveProductAsync(int id)
        {
            if(id == 0)
            {
                return false;
            }

            var product = await _productRepository.GetByIdAsync(id);

            if(product == null)
            {
                return false;
            }

            await _productRepository.DeleteAsync(product.Id);
            return true;
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
