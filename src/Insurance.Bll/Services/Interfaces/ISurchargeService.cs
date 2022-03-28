using Insurance.Api.Models;
using Insurance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Bll.Services.Interfaces
{
    public interface ISurchargeService
    {
        Task<IEnumerable<Surcharge>> GetAllAsync();
        Task<Surcharge> GetByProductTypeIdAsync(int productTypeId);
        Task<bool> InsertOrUpdateAsync(SurchargeDto surcharge);
    }
}
