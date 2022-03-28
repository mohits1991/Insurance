using Insurance.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Dal.Context
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Surcharge> Surcharges { get; set; }
    }
}
