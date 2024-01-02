using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Client> Clients { get; set; }

        public ConnectionContext(DbContextOptions<ConnectionContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}