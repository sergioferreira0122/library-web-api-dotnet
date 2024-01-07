using Library.Application.Abstractions;

namespace Library.Application.Features.Bookings.Queries
{
    public record GetAllBookingsQuery() : IQuery<ICollection<GetAllBookingsResponse>> { }
}