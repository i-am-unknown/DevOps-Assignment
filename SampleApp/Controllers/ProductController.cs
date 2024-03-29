using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly List<Product> _products = new List<Product>
{
            new Product { Id = 1, Name = "Product 1", Price = 19.99m },
            new Product { Id = 2, Name = "Product 2", Price = 29.99m },
            new Product { Id = 3, Name = "Product 3", Price = 9.99m }
}; 
        private int _nextId = 1;

        [HttpGet("GetProducts")]
        public IEnumerable<Product> Get()
        {
            return _products;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Return NotFoundResult if product is not found
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _products.Remove(product);
            return NoContent();
        }
    }
}
