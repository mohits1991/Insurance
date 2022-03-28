using Insurance.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Bll.Services.Interfaces
{
    public interface IInsuranceService
    {
        Task<ProductInsuranceDto> CalculateProductInsuranceAsync(int productId);
        Task<OrderInsuranceDto> CalculateOrderInsuranceAsync(IEnumerable<ProductDto> products);
    }
}
