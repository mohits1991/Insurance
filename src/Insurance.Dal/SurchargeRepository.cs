using Insurance.Dal.Context;
using Insurance.Dal.Entities;
using Insurance.Dal.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Dal
{
    /// <summary>
    /// Surcharge data access layer
    /// </summary>
    public class SurchargeRepository : ISurchargeRepository
    {
        private readonly InsuranceDbContext _dbContext;
        private readonly ILogger<SurchargeRepository> _logger;
        public SurchargeRepository(InsuranceDbContext dbContext, ILogger<SurchargeRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Get all surcharges
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Surcharge>> GetAllAsync()
        {
            try
            {
                return _dbContext.Surcharges;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }

        /// <summary>
        /// Get surcharge per producttypeid
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public async Task<Surcharge> GetByProductTypeIdAsync(int productTypeId)
        {
            try
            {
                return _dbContext.Surcharges.Where(x => x.ProductTypeId == productTypeId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }

        /// <summary>
        /// Insert ot update surcharge
        /// </summary>
        /// <param name="surcharge"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdateAsync(Surcharge surcharge)
        {
            try
            {
                var productSurcharge = await GetByProductTypeIdAsync(surcharge.ProductTypeId);
                if (productSurcharge != null && productSurcharge.ProductTypeId == surcharge.ProductTypeId)
                {
                    productSurcharge.Rate = surcharge.Rate;
                    _dbContext.Update(productSurcharge);
                }
                else
                {
                    _dbContext.Add(surcharge);
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex}");
                throw ex;
            }
        }
    }
}
