using Company.Application.InputModel;
using Company.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.ProducApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInputModel>>> Get()
        {
            var productInputModels = await _productService.GetProductsAsync();

            if (productInputModels is null)
            {
                return NotFound("products not found");
            }

            return Ok(productInputModels);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductInputModel>> Get(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return NotFound("Product not found");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductInputModel productInputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productInputModel is null)
            {
                return BadRequest("Invalid Data");
            }

            await _productService.AddProductAsync(productInputModel);

            return new CreatedAtRouteResult("GetProduct", new { id = productInputModel.Id }, productInputModel);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductInputModel productInputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productInputModel is null)
            {
                return BadRequest("Invalid Data");
            }

            await _productService.UpdateProductyAsync(productInputModel);

            return new CreatedAtRouteResult("GetProduct", new { id = productInputModel.Id }, productInputModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            var productInputModel = await _productService.GetProductByIdAsync(id);

            if (productInputModel is null)
            {
                return NotFound("Product not found");
            }

            await _productService.RemoveProductAsync(id);

            return Ok(productInputModel);
        }
    }
}
