using Donantes.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Donantes.API.Database
{
    public class BloodDonationDbContext : DbContext
    {
        public BloodDonationDbContext(
            DbContextOptions<BloodDonationDbContext> options
        ) : base(options)
        {
        }

        public DbSet<Donador> Donadores { get; set; }

        public DbSet<SolicitudSangre> SolicitudesSangre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Donador>()
                .HasIndex(d => d.Dni)
                .IsUnique();

            modelBuilder.Entity<SolicitudSangre>()
                .HasOne<Donador>()
                .WithMany()
                .HasForeignKey(s => s.DonadorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}