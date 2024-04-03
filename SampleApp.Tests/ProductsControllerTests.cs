using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApp.Controllers;
using SampleApp.Data;
using SampleApp.Model;


namespace SampleApp.Tests
{
    public class ProductsControllerTests
    {
        private readonly DbContextOptions<SampleDbContext> _options;

        public ProductsControllerTests()
        {
            _options = new DbContextOptionsBuilder<SampleDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Seed the database with test data
            using (var context = new SampleDbContext(_options))
            {
                context.Products.Add(new Product { ProductId = 1, ProductName = "Test Product", Price = 10.99m });
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_ForNonExistentId()
        {
            // Arrange
            using (var context = new SampleDbContext(_options))
            {
                var controller = new ProductController(context);

                // Act
                var result = await controller.GetProductById(999);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            // Arrange
            var newProduct = new Product { ProductName = "New Test Product", Price = 20.99m };
            using (var context = new SampleDbContext(_options))
            {
                var controller = new ProductController(context);

                // Act
                var result = await controller.CreateProduct(newProduct);

                // Assert
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                var createdProduct = Assert.IsType<Product>(createdAtActionResult.Value);
                Assert.Equal(newProduct.ProductName, createdProduct.ProductName);
            }
        }

        [Fact]
        public async Task Update_ReturnsNoContent_ForExistingProduct()
        {
            // Arrange
            var updatedProduct = new Product { ProductId = 1, ProductName = "Updated Product", Price = 30.99m };
            using (var context = new SampleDbContext(_options))
            {
                var controller = new ProductController(context);

                // Act
                var result = await controller.UpdateProduct(1, updatedProduct);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_ForExistingProduct()
        {
            // Arrange
            using (var context = new SampleDbContext(_options))
            {
                var controller = new ProductController(context);

                // Act
                var result = await controller.DeleteProduct(1);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }
        }
    }
}
