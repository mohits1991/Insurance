using System.Text.Json.Serialization;

namespace Insurance.Api.Models
{
    public class ProductInsuranceDto
    {
        public ProductDto Product { get; set; }
        public ProductTypeDto ProductType { get; set; }
        public bool HasInsurance { get { return InsuranceValue != 0; } }
        public double InsuranceValue { get; set; }
    }
}
