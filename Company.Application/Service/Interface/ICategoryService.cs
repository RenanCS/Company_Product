using Company.Application.InputModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Application.Service.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryInputModel>> GetCategoriesAsync();
    }
}
