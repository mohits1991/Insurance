using Insurance.Api.Models;
using Insurance.Bll.Services.Interfaces;
using Insurance.Dal.Entities;
using Insurance.Dal.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Insurance.Tests
{
    public class SurchargeServiceTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<ISurchargeRepository> _mockSurchargeRepository;
        private readonly Mock<ILogger<SurchargeService>> _mockLogger;
        private readonly ISurchargeService _surchargeService;
        public SurchargeServiceTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockSurchargeRepository = new Mock<ISurchargeRepository>();
            _mockLogger = new Mock<ILogger<SurchargeService>>();
            _surchargeService = new SurchargeService(_mockProductService.Object, _mockSurchargeRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetAllSurcharges_ShouldGetAllSurchargesWithProductType()
        {
            var surcharges = new List<Surcharge>() { new Surcharge { Id = 1, ProductTypeId = 1, Rate = 1 },
                                    new Surcharge { Id = 2, ProductTypeId = 2, Rate = 2 },
                                    new Surcharge { Id = 3, ProductTypeId = 3, Rate = 3 }};
            _mockSurchargeRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(surcharges);

            var result = _surchargeService.GetAllAsync().Result as List<Surcharge>;

            Assert.Equal(
                expected: surcharges.Count,
                actual: result.Count
            );
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void GetSurcharge_WhenProductTypeIdIsPassed_ShouldGetSurchargeForProductType(int productTypeId, double expectedRateValue)
        {
            _mockSurchargeRepository.Setup(repo => repo.GetByProductTypeIdAsync(1))
            .ReturnsAsync(new Surcharge { Id = 1, ProductTypeId = 1, Rate = 1 });

            _mockSurchargeRepository.Setup(repo => repo.GetByProductTypeIdAsync(2))
           .ReturnsAsync(new Surcharge { Id = 2, ProductTypeId = 2, Rate = 2 });

            _mockSurchargeRepository.Setup(repo => repo.GetByProductTypeIdAsync(3))
           .ReturnsAsync(new Surcharge { Id = 3, ProductTypeId = 3, Rate = 3 });

            var result = _surchargeService.GetByProductTypeIdAsync(productTypeId).Result;

            Assert.Equal(
                expected: expectedRateValue,
                actual: result.Rate
            );
        }

        [Fact]
        public void InsertOrUpdateSurcharge_WhenSurchargeIsPassed_SurchargeShouldBeAddedForProductType()
        {
            _mockProductService.Setup(service => service.GetProductTypeByIdAsync(1))
            .ReturnsAsync(new ProductTypeDto { Id = 1, Name = "P1", CanBeInsured = true });

           // _mockProductService.Setup(service => service.GetProductTypeByIdAsync(2))
           //.ReturnsAsync(new ProductTypeDto { Id = 2, Name = "P2", CanBeInsured = true });

           // _mockProductService.Setup(service => service.GetProductTypeByIdAsync(3))
           //.ReturnsAsync(new ProductTypeDto { Id = 3, Name = "P3", CanBeInsured = true });

            _mockSurchargeRepository.Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<Surcharge>()))
            .ReturnsAsync(true);

            var result = _surchargeService.InsertOrUpdateAsync(new SurchargeDto { Id = 1, ProductTypeId = 1, Rate = 1 }).Result;

            Assert.Equal(
                expected: true,
                actual: result
            );
        }
    }
}