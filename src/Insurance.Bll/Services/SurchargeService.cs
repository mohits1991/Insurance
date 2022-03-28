using Insurance.Api.Models;
using Insurance.Dal.Entities;
using Insurance.Dal.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Bll.Services.Interfaces
{
    /// <summary>
    /// Surcharge related logic
    /// </summary>
    public class SurchargeService : ISurchargeService
    {
        private readonly ISurchargeRepository _surchargeRepository;
        private readonly IProductService _productService;
        private readonly ILogger<SurchargeService> _logger;
        public SurchargeService(IProductService productService, ISurchargeRepository surchargeRepository, ILogger<SurchargeService> logger)
        {
            _productService = productService;
            _surchargeRepository = surchargeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get all surcharges
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Surcharge>> GetAllAsync()
        {
            try
            {
                return _surchargeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }

        /// <summary>
        /// Get sucrcharge by producttypeid
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public Task<Surcharge> GetByProductTypeIdAsync(int productTypeId)
        {
            try
            {
                return _surchargeRepository.GetByProductTypeIdAsync(productTypeId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }

        /// <summary>
        /// Add or update surcharge
        /// </summary>
        /// <param name="surcharge"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdateAsync(SurchargeDto surcharge)
        {
            try
            {
                var productType = await _productService.GetProductTypeByIdAsync(surcharge.ProductTypeId);
                if (productType.Id == 0 || productType.Id != surcharge.ProductTypeId)
                {
                    return false;
                }

                return await _surchargeRepository.InsertOrUpdateAsync(new Surcharge { ProductTypeId = surcharge.ProductTypeId, Rate = surcharge.Rate });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }
    }
}
