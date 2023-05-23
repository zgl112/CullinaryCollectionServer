using baseAPI.Contracts;
using baseAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace baseAPI.Controllers
{
    //Authorised API endpoints for CRUD operations
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _productService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAllProducts(string id, string search)
        {
            var products = await _productService.SearchAllProducts(id, search);
            return Ok(products);
        }

        [HttpPost("create")]
        [Authorize]

        public async Task<IActionResult> CreateAsync([FromBody] Product product)
        {
            var result = await _productService.CreateAsync(product);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Update(string id, [FromBody] Product product)
        {
            var result = await _productService.UpdateAsync(id, product);
          
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(string id)
        {
            await _productService.DeleteAsync(id);
            
            return NoContent();
        }
    }
}
