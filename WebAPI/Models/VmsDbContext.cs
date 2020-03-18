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
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverVehicleMessage> DriverVehicleMessages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //creating necessary indexes
            modelBuilder.Entity<Driver>()
                .HasIndex(b => b.UserName)
                .IsUnique()
                .IncludeProperties(p => new
                {
                   p.Password
                }); 

            modelBuilder.Entity<Vehicle>()
                .HasIndex(b => b.PlateNumber)
                .IsUnique();

            modelBuilder.Entity<DriverVehicleMessage>()
               .HasIndex(p => new { p.DriverId, p.VehicleId, p.CreatedAt });

            modelBuilder.Entity<Driver>()
                .HasIndex(p => new { p.DrivingLicenseNumber, p.LicenseCountryCode })
                .IsUnique();


            //json property
            modelBuilder.Entity<Vehicle>()
                .Property(b => b._connectionDetails).HasColumnName("ConnectionDetails");


            //delete behaviour restrict
            modelBuilder.Entity<DriverVehicleMessage>()
                .HasOne(u => u.Driver)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DriverVehicleMessage>()
              .HasOne(u => u.Vehicle)
              .WithMany()
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

        }


        //before saving record in database
        //set createdAt and updatedAt used in BaseEntity base class of models
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
