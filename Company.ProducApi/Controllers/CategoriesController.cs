using Company.Application.InputModel;
using Company.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.ProducApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryInputModel>>> Get()
        {
            var categoriesDto = await _categoryService.GetCategoriesAsync();

            if (categoriesDto is null)
            {
                return NotFound("Categories not found");
            }

            return Ok(categoriesDto);
        }
    }
}
