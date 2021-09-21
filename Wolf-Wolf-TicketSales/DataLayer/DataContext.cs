using Microsoft.EntityFrameworkCore;

namespace Wolf_Wolf_TicketSales.DataLayer
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserTicket> UserTickets { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            modelBuilder.Entity<UserTicket>()
                .HasKey(x => new { x.ConcertId, x.UserId});
        }
    }
}
