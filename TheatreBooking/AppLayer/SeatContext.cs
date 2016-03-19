using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TheatreBooking.AppLayer
{
    public class SeatContext : DbContext
    {

        public SeatContext() : base("DefaultConnection")
        {
        }

        public DbSet<Booker> Bookers { get; set; }
        public DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}