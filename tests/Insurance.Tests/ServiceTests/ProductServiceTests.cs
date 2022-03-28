using Insurance.Api.Models;
using Insurance.Bll;
using Insurance.Bll.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace Insurance.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IRequestService> _mockRequestService;
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        private readonly IProductService _productService;
        public ProductServiceTests()
        {
            _mockRequestService = new Mock<IRequestService>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _productService = new ProductService(_mockRequestService.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetAllProducts_ShouldGetAllProducts()
        {
            var products = new List<ProductDto>() { new ProductDto { Id = 1,  Name= "P1",ProductTypeId=1, SalesPrice= 1 },
                                    new ProductDto { Id = 2,  Name= "P2",ProductTypeId=2, SalesPrice= 2 },
                                    new ProductDto { Id = 3,  Name= "P3",ProductTypeId=3, SalesPrice= 3 }};
            string jsonString = JsonConvert.SerializeObject(products);
            _mockRequestService.Setup(service => service.GetAsync("/products")).ReturnsAsync(jsonString);

            var result = _productService.GetProductsAsync().Result as List<ProductDto>;

            Assert.Equal(
                expected: products.Count,
                actual: result.Count
            );
        }

        [Fact]
        public void GetAllProductTypes_ShouldGetAllProductTypes()
        {
            var productTypes = new List<ProductTypeDto>() { new ProductTypeDto { Id = 1,  Name= "P1", CanBeInsured = true },
                                    new ProductTypeDto { Id = 2,  Name= "P2", CanBeInsured = true},
                                    new ProductTypeDto { Id = 3,  Name= "P3", CanBeInsured = true }};

            string jsonString = JsonConvert.SerializeObject(productTypes);
            _mockRequestService.Setup(service => service.GetAsync("/product_types")).ReturnsAsync(jsonString);

            var result = _productService.GetProductTypeAsync().Result as List<ProductTypeDto>;

            Assert.Equal(
                expected: productTypes.Count,
                actual: result.Count
            );
        }

        [Fact]
        public void GetProduct_WhenProductIdIsPassed_ShouldGetProduct()
        {
            var product = new ProductDto { Id = 1, Name = "P1", ProductTypeId = 1, SalesPrice = 1 };
            string jsonString = JsonConvert.SerializeObject(product);
            _mockRequestService.Setup(service => service.GetAsync(It.IsAny<string>())).ReturnsAsync(jsonString);

            var result = _productService.GetProductByIdAsync(1).Result;

            Assert.Equal(
                expected: product.Name,
                actual: result.Name
            );
        }

        [Fact]
        public void GetProductType_WhenProductTypeIdIsPassed_ShouldGetProductType()
        {
            var productType = new ProductTypeDto { Id = 1, Name = "P1", CanBeInsured = true };
            string jsonString = JsonConvert.SerializeObject(productType);
            _mockRequestService.Setup(service => service.GetAsync(It.IsAny<string>())).ReturnsAsync(jsonString);

            var result = _productService.GetProductByIdAsync(1).Result;

            Assert.Equal(
                expected: productType.Name,
                actual: result.Name
            );
        }
    }
}