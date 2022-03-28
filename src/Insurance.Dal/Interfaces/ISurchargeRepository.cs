using Insurance.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Dal.Interfaces
{
    public interface ISurchargeRepository
    {
        Task<IEnumerable<Surcharge>> GetAllAsync();
        Task<Surcharge> GetByProductTypeIdAsync(int productTypeId);
        Task<bool> InsertOrUpdateAsync(Surcharge surcharge);
    }
}
