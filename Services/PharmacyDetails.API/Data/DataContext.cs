using Microsoft.EntityFrameworkCore;
using PharmacyDetails.API.Entities;

namespace PharmacyDetails.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<PharmacyDetail> pharmacyDetails { get; set; }
    }
}
