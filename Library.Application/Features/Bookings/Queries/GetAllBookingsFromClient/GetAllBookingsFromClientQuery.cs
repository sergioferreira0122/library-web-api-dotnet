using Library.Application.Abstractions;

namespace Library.Application.Features.Bookings.Queries
{
    public record GetAllBookingsFromClientQuery(int clientId) : IQuery<ICollection<GetAllBookingsFromClientResponse>?>{}
}
