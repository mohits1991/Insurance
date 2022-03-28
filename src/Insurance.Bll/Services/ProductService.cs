using Insurance.Api.Models;
using Insurance.Bll.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Bll
{
    /// <summary>
    /// Product related logic
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IRequestService _requestService;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IRequestService requestService, ILogger<ProductService> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }

        /// <summary>
        /// Get product by product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            try
            {
                return JsonConvert.DeserializeObject<ProductDto>(await _requestService.GetAsync(string.Format("/products/{0:G}", productId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(await _requestService.GetAsync("/products"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }

        /// <summary>
        /// Get all product types
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductTypeDto>> GetProductTypeAsync()
        {
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductTypeDto>>(await _requestService.GetAsync("/product_types"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }

        /// <summary>
        /// Get product type by producttypeid
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public async Task<ProductTypeDto> GetProductTypeByIdAsync(int productTypeId)
        {
            try
            {
                return JsonConvert.DeserializeObject<ProductTypeDto>(await _requestService.GetAsync(string.Format("/product_types/{0:G}", productTypeId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }
    }
}
