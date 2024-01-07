using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Bookings.Queries
{
    public class GetAllBookingsFromClientMapper : IMapper<ICollection<Booking>, ICollection<GetAllBookingsFromClientResponse>>
    {
        public ICollection<GetAllBookingsFromClientResponse> Map(
            ICollection<Booking> data,
            ICollection<GetAllBookingsFromClientResponse> target)
        {
            if (data.Count == 0)
                return target;

            foreach (var booking in data)
            {
                target.Add(IndividualMapping(booking));
            }

            return target;
        }

        private static GetAllBookingsFromClientResponse IndividualMapping(Booking data)
        {
            var readModel = new GetAllBookingsFromClientResponse
            {
                BookTitle = data.Book!.Title,
                BookAuthor = data.Book!.Author,
                IssueDate = DateOnly.FromDateTime(data.IssueDate!.Value)
            };

            return readModel;
        }
    }
}