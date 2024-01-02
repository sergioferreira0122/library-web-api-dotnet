using Library.Domain.Entities;

namespace Library.Domain.Abstractions
{
    public interface IBookingRepository
    {
        Task<ICollection<Booking>> GetBookingsAsync(CancellationToken cancellationToken);

        Task<Booking?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task AddBookingAsync(Booking booking, CancellationToken cancellationToken);

        Task DeleteBookingAsync(Booking booking);

        Task UpdateBookingAsync(Booking booking);

        Task<ICollection<Booking>> GetBookingsFromClientIdAsync(int clientId, CancellationToken cancellationToken);
    }
}