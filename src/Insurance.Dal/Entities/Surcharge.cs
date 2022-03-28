namespace Insurance.Dal.Entities
{
    public class Surcharge
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public double Rate { get; set; }
    }
}
