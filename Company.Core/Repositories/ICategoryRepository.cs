using Company.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
