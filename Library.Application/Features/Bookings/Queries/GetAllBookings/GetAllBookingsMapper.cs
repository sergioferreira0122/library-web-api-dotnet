using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Bookings.Queries
{
    public class GetAllBookingsMapper : IMapper<ICollection<Booking>, ICollection<GetAllBookingsResponse>>
    {
        public ICollection<GetAllBookingsResponse> Map(
            ICollection<Booking> data,
            ICollection<GetAllBookingsResponse> target)
        {
            if (data.Count == 0)
                return target;

            foreach (var booking in data)
            {
                target.Add(IndividualMapping(booking));
            }

            return target;
        }

        private static GetAllBookingsResponse IndividualMapping(Booking data)
        {
            var readModel = new GetAllBookingsResponse
            {
                Id = data.Id,
                BookTitle = data.Book!.Title,
                ClientName = data.Client!.Name,
                IssuedDate = DateOnly.FromDateTime(data.IssueDate!.Value)
            };

            return readModel;
        }
    }
}