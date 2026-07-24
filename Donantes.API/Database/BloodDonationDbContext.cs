using Microsoft.EntityFrameworkCore;
using Donantes.API.Entities;

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
    }
}