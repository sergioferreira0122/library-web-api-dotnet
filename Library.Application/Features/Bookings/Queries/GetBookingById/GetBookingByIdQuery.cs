using Library.Application.Abstractions;

namespace Library.Application.Features.Bookings.Queries
{
    public record GetBookingByIdQuery(int Id) : IQuery<GetBookingByIdResponse?> {}
}
