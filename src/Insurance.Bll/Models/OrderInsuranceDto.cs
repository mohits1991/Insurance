using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Insurance.Api.Models
{
    public class OrderInsuranceDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public double OrderInsuranceValue { get; set; }
    }
}
