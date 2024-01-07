using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ConnectionContext _dbContext;

        public BookingRepository(ConnectionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBookingAsync(
            Booking booking,
            CancellationToken cancellationToken)
        {
            await _dbContext.Bookings.AddAsync(booking, cancellationToken);
            _dbContext.Books.Attach(booking.Book!);
            _dbContext.Clients.Attach(booking.Client!);
        }

        public Task DeleteBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Remove(booking);
            return Task.CompletedTask;
        }

        public async Task<ICollection<Booking>> GetBookingsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Bookings
                .Include("Book")
                .Include("Client")
                .ToListAsync(cancellationToken);
        }

        public async Task<ICollection<Booking>> GetBookingsFromClientIdAsync(
            int clientId,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Bookings
                .Include("Book")
                .Include("Client")
                .Where(booking => booking.Client!.Id == clientId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Booking?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Bookings
                .Include("Book")
                .Include("Client")
                .FirstOrDefaultAsync(booking => booking.Id == id, cancellationToken);
        }

        public Task UpdateBookingAsync(Booking booking)
        {
            _dbContext.Bookings.Entry(booking).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}