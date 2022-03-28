using Insurance.Api.Controllers;
using Insurance.Api.Models;
using Insurance.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceControllerTests
    {
        private readonly Mock<IInsuranceService> _mockInsuranceService;
        private readonly Mock<ILogger<InsuranceController>> _mockLogger;
        private readonly InsuranceController _insuranceController;
        public InsuranceControllerTests()
        {
            _mockInsuranceService = new Mock<IInsuranceService>();
            _mockLogger = new Mock<ILogger<InsuranceController>>();
            _insuranceController = new InsuranceController(_mockInsuranceService.Object, _mockLogger.Object);
        }

        [Theory]
        [InlineData(1, 1000)]
        [InlineData(2, 500)]
        [InlineData(3, 0)]
        [InlineData(4, 2000)]
        public void CalculateProductInsurance_GivenSalesPrice_ShouldGetRightProductInsuranceCost(int productId, float expectedInsuranceValue)
        {
            _mockInsuranceService.Setup(service => service.CalculateProductInsuranceAsync(1))
           .ReturnsAsync(new ProductInsuranceDto() { Product = new ProductDto { Id = 1, SalesPrice = 699, ProductTypeId = 12 }, InsuranceValue = 1000 });
            _mockInsuranceService.Setup(service => service.CalculateProductInsuranceAsync(2))
           .ReturnsAsync(new ProductInsuranceDto() { Product = new ProductDto { Id = 2, SalesPrice = 299, ProductTypeId = 33 }, InsuranceValue = 500 });
            _mockInsuranceService.Setup(service => service.CalculateProductInsuranceAsync(3))
           .ReturnsAsync(new ProductInsuranceDto() { Product = new ProductDto { Id = 3, SalesPrice = 199, ProductTypeId = 21 }, InsuranceValue = 0 });
            _mockInsuranceService.Setup(service => service.CalculateProductInsuranceAsync(4))
           .ReturnsAsync(new ProductInsuranceDto() { Product = new ProductDto { Id = 4, SalesPrice = 2999, ProductTypeId = 32 }, InsuranceValue = 2000 });

            var result = _insuranceController.CalculateProductInsurance(productId).Result as OkObjectResult;

            Assert.Equal(
               expected: 200,
               actual: result.StatusCode
            );

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: (result.Value as ProductInsuranceDto).InsuranceValue
            );
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 1000)]
        [InlineData(new int[] { 1, 2, 3, 4 }, 1500)]
        [InlineData(new int[] { 1, 2 }, 500)]
        public void CalculateOrderInsurance_GivenProducts_ShouldGetRightOrderInsuranceCost(int[] productIds, double expectedInsuranceValue)
        {
            var products = new List<ProductDto>();
            foreach (var productId in productIds)
            {
                products.Add(new ProductDto() { Id = productId });
            }

            _mockInsuranceService.Setup(service => service.CalculateOrderInsuranceAsync(products))
            .ReturnsAsync(new OrderInsuranceDto() { OrderInsuranceValue = expectedInsuranceValue });

            var result = _insuranceController.CalculateOrderInsurance(products).Result as OkObjectResult;

            Assert.Equal(
               expected: 200,
               actual: result.StatusCode
            );

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: (result.Value as OrderInsuranceDto).OrderInsuranceValue
            );
        }
    }
}