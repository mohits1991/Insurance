using Insurance.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Bll.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductTypeDto>> GetProductTypeAsync();
        Task<ProductTypeDto> GetProductTypeByIdAsync(int productTypeId);
    }
}
