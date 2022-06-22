using AutoMapper;
using Company.Application.InputModel;
using Company.Application.Service.Interface;
using Company.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryInputModel>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryInputModel>>(categories);
        }
    }
}
