using Microsoft.EntityFrameworkCore;
using TMAWWarehouse.Services.TMARequests.Models;

namespace TMAWWarehouse.Services.TMARequests.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TMARequest> TMARequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
