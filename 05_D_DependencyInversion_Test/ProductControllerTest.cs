using _05_D_DependencyInversion;
using System;
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
    }
}
