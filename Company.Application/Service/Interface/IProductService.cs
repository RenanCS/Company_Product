using Company.Application.InputModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Application.Service.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductInputModel>> GetProductsAsync();
        Task<ProductInputModel> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductInputModel productDTO);
        Task UpdateProductyAsync(ProductInputModel productDTO);
        Task RemoveProductAsync(int id);
    }
}
