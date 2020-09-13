using _05_D_DependencyInversion;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace _05_D_DependencyInversion_Test
{
    public class ProductControllerTest
    {
        [Fact]
        public void Product_Sample_Not_Empty()
        {
            // arrange
            var loger = new AppLogger();
            var productRepo = new ProductRepository();
            var productController = new ProductController(loger, productRepo);                       

            // act
            var result = productController.GetAll();

            // assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void Product_Should_Return_Success()
        {
            // arrange
            var loger = new AppLogger();
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(x => x.GetAll())
                .Returns(new List<Product>() { new Product { Id = 1, Name = "PC", Price = 1000 } });
            var productController = new ProductController(loger, mockProductRepo.Object);

            // act
            var result = productController.GetAll();

            // assert
            Assert.Equal(1000, result.First().Price);
        }
    }
}
