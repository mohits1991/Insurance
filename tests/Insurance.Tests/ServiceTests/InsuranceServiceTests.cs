using Insurance.Api.Models;
using Insurance.Bll;
using Insurance.Bll.Services.Interfaces;
using Insurance.Dal.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceServiceTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ISurchargeService> _mockSurchargeService;
        private readonly Mock<ILogger<InsuranceService>> _mockLogger;
        private readonly IInsuranceService _insuranceService;
        public InsuranceServiceTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockSurchargeService = new Mock<ISurchargeService>();
            _mockLogger = new Mock<ILogger<InsuranceService>>();
            _insuranceService = new InsuranceService(_mockProductService.Object, _mockSurchargeService.Object, _mockLogger.Object);
        }

        [Theory]
        [InlineData(1, 1000)]
        [InlineData(2, 0)]
        [InlineData(3, 500)]
        public void CalculateProductInsurance_GivenSalesPrice_ShouldGetRightProductInsuranceCost(int productId, float expectedInsuranceValue)
        {
            _mockProductService.Setup(service => service.GetProductByIdAsync(1))
            .ReturnsAsync(new ProductDto { Id = 1, SalesPrice = 699, ProductTypeId = 11 });
            _mockProductService.Setup(service => service.GetProductTypeByIdAsync(11))
            .ReturnsAsync(new ProductTypeDto { Id = 11, Name = "ProductType11", CanBeInsured = true });

            _mockProductService.Setup(service => service.GetProductByIdAsync(2))
            .ReturnsAsync(new ProductDto { Id = 2, SalesPrice = 4000, ProductTypeId = 12 });
            _mockProductService.Setup(service => service.GetProductTypeByIdAsync(12))
            .ReturnsAsync(new ProductTypeDto { Id = 12, Name = "ProductType12", CanBeInsured = false });

            _mockProductService.Setup(service => service.GetProductByIdAsync(3))
            .ReturnsAsync(new ProductDto { Id = 3, SalesPrice = 299, ProductTypeId = 32 });
            _mockProductService.Setup(service => service.GetProductTypeByIdAsync(32))
            .ReturnsAsync(new ProductTypeDto { Id = 32, Name = "ProductType32", CanBeInsured = true });

            var result = _insuranceService.CalculateProductInsuranceAsync(productId).Result;

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.InsuranceValue
            );
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 1700)]
        [InlineData(new int[] { 1, 2 }, 1200)]
        [InlineData(new int[] { 1 }, 1100)]
        [InlineData(new int[] { 1, 2, 2, 2, 3, 3 }, 2200)]
        public void CalculateOrderInsurance_GivenProducts_ShouldGetRightOrderInsuranceCost(int[] productIds, double expectedInsuranceValue)
        {
            _mockSurchargeService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<Surcharge>(){
                            new Surcharge { Id = 1, ProductTypeId = 11, Rate = 100 },
                            new Surcharge { Id = 2, ProductTypeId = 12, Rate = 100 }});

            _mockProductService.Setup(service => service.GetProductByIdAsync(1))
            .ReturnsAsync(new ProductDto { Id = 1, SalesPrice = 699, ProductTypeId = 11 });
            _mockProductService.Setup(service => service.GetProductTypeByIdAsync(11))
            .ReturnsAsync(new ProductTypeDto { Id = 11, Name = "ProductType11", CanBeInsured = true });

            _mockProductService.Setup(service => service.GetProductByIdAsync(2))
            .ReturnsAsync(new ProductDto { Id = 2, SalesPrice = 4000, ProductTypeId = 12 });
            _mockProductService.Setup(service => service.GetProductTypeByIdAsync(12))
            .ReturnsAsync(new ProductTypeDto { Id = 12, Name = "ProductType12", CanBeInsured = false });

            _mockProductService.Setup(service => service.GetProductByIdAsync(3))
            .ReturnsAsync(new ProductDto { Id = 3, SalesPrice = 299, ProductTypeId = 32 });
            _mockProductService.Setup(service => service.GetProductTypeByIdAsync(32))
            .ReturnsAsync(new ProductTypeDto { Id = 32, Name = "ProductType13", CanBeInsured = true });

            var products = new List<ProductDto>();
            foreach (var productId in productIds)
            {
                products.Add(new ProductDto() { Id = productId });
            }

            var result = _insuranceService.CalculateOrderInsuranceAsync(products).Result;

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.OrderInsuranceValue
            );
        }
    }
}