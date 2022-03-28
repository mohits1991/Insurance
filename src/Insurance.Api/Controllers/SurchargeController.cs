using Insurance.Api.Models;
using Insurance.Bll.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurchargeController : ControllerBase
    {
        private readonly ISurchargeService _surchargeService;
        private readonly ILogger<InsuranceController> _logger;
        public SurchargeController(ISurchargeService surchargeService, ILogger<InsuranceController> logger)
        {
            _surchargeService = surchargeService;
            _logger = logger;
        }

        /// <summary>
        /// Get surcharge for a product type
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{productTypeId}")]
        public async Task<IActionResult> Get(int productTypeId)
        {
            try
            {
                _logger.LogInformation($"Fetching surcharge per product type for {productTypeId}.");
                var data = await _surchargeService.GetByProductTypeIdAsync(productTypeId);
                if (data == null)
                {
                    _logger.LogInformation($"Product type surcharge not found for {productTypeId}");
                    return NotFound("Surcharge not found.");
                }
                _logger.LogInformation($"Surcharge fetched for {productTypeId}");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Fetch all surcharges
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation($"Fetching all surcharges.");
                var data = await _surchargeService.GetAllAsync();
                _logger.LogInformation($"All surcarges fetched.");
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Add or update surcharge per product type
        /// </summary>
        /// <param name="surcharge"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadSurchargeRatePerProductType(SurchargeDto surcharge)
        {
            try
            {
                _logger.LogInformation($"Uploading surcharge for {surcharge}.");
                var data = await _surchargeService.InsertOrUpdateAsync(surcharge);
                _logger.LogInformation($"Surcharge uploaded for {surcharge}");
                if (data)
                {
                    return StatusCode(201, "Updation successful.");
                }
                else
                {
                    _logger.LogInformation($"Surcharge can not be added for {surcharge}");
                    return StatusCode(400, "Bad Request.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}