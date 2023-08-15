using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public IActionResult GetProductById(int productId)
        {
            var product = _productService.GetProductById(productId);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductRequest productDto)
        {
            var createdProduct = _productService.CreateProduct(productDto);
            return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(int productId, ProductRequest productDto)
        {
            var updatedProduct = _productService.UpdateProduct(productId, productDto);

            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            _productService.DeleteProduct(productId);
            return NoContent();
        }
    }
}
