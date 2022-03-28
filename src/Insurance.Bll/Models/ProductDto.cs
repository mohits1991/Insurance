using System.Text.Json.Serialization;

namespace Insurance.Api.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string Name { get; set; }
        [JsonIgnore]
        public double SalesPrice { get; set; }
        [JsonIgnore]
        public int ProductTypeId { get; set; }
    }
}