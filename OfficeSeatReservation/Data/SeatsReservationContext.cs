
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
        public Reservation Reservation { get; set; }
        public Seat Seat { get; set; }
        public DbSet<Reservation> Reservations { get; set; }  
        
        public DbSet<Seat> Seats { get; set; }


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
