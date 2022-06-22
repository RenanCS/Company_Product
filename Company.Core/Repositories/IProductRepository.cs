using Company.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Core.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<Product> DeleteAsync(int id);
    }
}
