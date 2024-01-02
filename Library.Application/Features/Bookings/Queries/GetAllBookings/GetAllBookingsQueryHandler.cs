using Library.Application.Abstractions;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Bookings.Queries
{
    public class GetAllBookingsQueryHandler : IQueryHandler<GetAllBookingsQuery, ICollection<GetAllBookingsResponse>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<ICollection<Booking>, ICollection<GetAllBookingsResponse>> _mapper;

        public GetAllBookingsQueryHandler(
            IBookingRepository bookingRepository,
            IMapper<ICollection<Booking>, ICollection<GetAllBookingsResponse>> mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllBookingsResponse>> Handle(
            GetAllBookingsQuery request,
            CancellationToken cancellationToken)
        {
            var books = await _bookingRepository.GetBookingsAsync(cancellationToken);

            return _mapper.Map(books, new List<GetAllBookingsResponse>());
        }
    }
}