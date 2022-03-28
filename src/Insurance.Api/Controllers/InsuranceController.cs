using Insurance.Api.Models;
using Insurance.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    /// <summary>
    /// Insurance related requests
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;
        private readonly ILogger<InsuranceController> _logger;
        public InsuranceController(IInsuranceService insuranceService, ILogger<InsuranceController> logger)
        {
            _insuranceService = insuranceService;
            _logger = logger;
        }

        /// <summary>
        /// Calculate insurance per product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/{id}")]
        public async Task<IActionResult> CalculateProductInsurance(int id)
        {
            try
            {
                _logger.LogInformation($"Calculating product insurance for {id}.");

                var data =await _insuranceService.CalculateProductInsuranceAsync(id);

                if(data == null || data.Product.Id == 0)
                {
                    _logger.LogInformation($"Product not found - {id}");
                    return NotFound("Product not found.");
                }

                _logger.LogInformation($"Product insurance calculated for {data.Product.Id}");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Calculate insurance for order
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("order")]
        public async Task<IActionResult> CalculateOrderInsurance(IEnumerable<ProductDto> products)
        {
            try
            {
                _logger.LogInformation($"Calculating order insurance for {products}.");
                var data = await _insuranceService.CalculateOrderInsuranceAsync(products);
                _logger.LogInformation($"Order insurance calculated for {products}");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}