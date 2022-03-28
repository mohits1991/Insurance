using System.Text.Json.Serialization;

namespace Insurance.Api.Models
{
    public class SurchargeDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public double Rate { get; set; }
    }
}
