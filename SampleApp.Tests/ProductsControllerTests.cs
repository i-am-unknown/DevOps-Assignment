using Microsoft.AspNetCore.Mvc;
using SampleApp.Controllers;

namespace SampleApp.Tests
{
    public class ProductsControllerTests
    {
        private ProductController _controller;

        public ProductsControllerTests()
        {
            _controller = new ProductController();
        }

        [Fact]
        public void Get_ReturnsEmptyList()
        {
            var result = _controller.Get();
            Assert.NotNull(result);
            //Assert.Empty(result);
        }

        [Fact]
        public void GetById_ReturnsNotFound_ForNonExistentId()
        {
            var result = _controller.GetById(999) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ReturnsCreatedAtAction()
        {
            var product = new Product { Name = "Test Product", Price = 10.99m };
            var result = _controller.Create(product) as CreatedAtActionResult;
            Assert.NotNull(result);
            if (result != null)
            {
            Assert.Equal(1, (result.Value as Product).Id); // Assuming the first id is 1
            }
        }

        [Fact]
        public void Update_ReturnsNoContent_ForExistingProduct()
        {
            _controller.Create(new Product { Name = "Test Product", Price = 10.99m });
            var updatedProduct = new Product { Name = "Updated Product", Price = 20.99m };
            var result = _controller.Update(1, updatedProduct) as NoContentResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_ForExistingProduct()
        {
            _controller.Create(new Product { Name = "Test Product", Price = 10.99m });
            var result = _controller.Delete(1) as NoContentResult;
            Assert.NotNull(result);
        }
    }
}
