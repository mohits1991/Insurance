using Insurance.Api.Models;
using Insurance.Bll.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Bll
{
    /// <summary>
    /// Insurance related logic
    /// </summary>
    public class InsuranceService : IInsuranceService
    {
        private readonly IProductService _productService;
        private readonly ISurchargeService _surchargeService;
        private readonly ILogger<InsuranceService> _logger;
        public InsuranceService(IProductService productService, ISurchargeService surchargeService, ILogger<InsuranceService> logger)
        {
            _productService = productService;
            _surchargeService = surchargeService;
        }

        /// <summary>
        /// Calculate insurance for product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ProductInsuranceDto> CalculateProductInsuranceAsync(int productId)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(productId);
                var productType = await _productService.GetProductTypeByIdAsync(product.ProductTypeId);

                double insuranceValue = 0;

                if (productType.CanBeInsured)
                {
                    if (product.SalesPrice >= 500 && product.SalesPrice < 2000)
                    {
                        insuranceValue += 1000;
                    }
                    else if (product.SalesPrice >= 2000)
                    {
                        insuranceValue += 2000;
                    }

                    //21 for Laptops and 32 for Smartphones
                    if (productType.Id == 21 || productType.Id == 32)
                    {
                        insuranceValue += 500;
                    }
                }

                return new ProductInsuranceDto() { Product = product, ProductType = productType, InsuranceValue = insuranceValue };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }

        /// <summary>
        /// Calculate insurance for order
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public async Task<OrderInsuranceDto> CalculateOrderInsuranceAsync(IEnumerable<ProductDto> products)
        {
            try
            {
                var totalInsurance = 0.0;
                int digitalCameraCount = 0;
                var productTypeIds = new List<int>();
                var order = new OrderInsuranceDto() { Products = products };
                foreach (var product in products)
                {
                    var insurance = await CalculateProductInsuranceAsync(product.Id);
                    productTypeIds.Add(insurance.Product.ProductTypeId);
                    if (insurance.HasInsurance)
                    {
                        totalInsurance += insurance.InsuranceValue;
                    }

                    //Task 4 - one or more digital cameras (prducttypeid 33)
                    if (insurance.Product.ProductTypeId == 33)
                    {
                        digitalCameraCount++;
                    }
                }

                //Task 4 - one or more digital cameras
                if (digitalCameraCount > 0)
                {
                    totalInsurance += 500;
                }

                // adding surcharge
                var surcharge =(await _surchargeService.GetAllAsync()).Where(x => productTypeIds.Contains(x.ProductTypeId)).Sum(x => x.Rate);
                order.OrderInsuranceValue = totalInsurance + surcharge;

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }
    }
}
