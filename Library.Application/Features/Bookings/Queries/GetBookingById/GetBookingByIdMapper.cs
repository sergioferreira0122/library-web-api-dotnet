using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Bookings.Queries
{
    public class GetBookingByIdMapper : IMapper<Booking, GetBookingByIdResponse>
    {
        public GetBookingByIdResponse Map(
            Booking data,
            GetBookingByIdResponse target)
        {
            target.Id = data.Id;
            target.BookTitle = data.Book!.Title;
            target.ClientName = data.Client!.Name;
            target.IssuedDate = DateOnly.FromDateTime(data.IssueDate!.Value);

            return target;
        }
    }
}