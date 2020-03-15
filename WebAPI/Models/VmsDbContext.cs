 using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class VmsDbContext : DbContext
    {
        public VmsDbContext(DbContextOptions<VmsDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {


            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }

}
