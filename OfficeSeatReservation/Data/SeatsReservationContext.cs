
using Microsoft.EntityFrameworkCore;
using OfficeSeatReservation.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace OfficeSeatReservation.Data
{
    public class SeatsReservationContext : DbContext
    {
        public SeatsReservationContext(DbContextOptions<SeatsReservationContext> options): base(options)
        {
            
        }
        public SeatsReservationContext()
        {
            
        }
        public DbSet<Reservation> Reservations { get; set; }  
        
        public DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
