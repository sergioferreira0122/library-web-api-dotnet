using Library.Application.Abstractions;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Bookings.Queries
{
    public class GetBookingByIdQueryHandler : IQueryHandler<GetBookingByIdQuery, GetBookingByIdResponse?>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<Booking, GetBookingByIdResponse> _mapper;

        public GetBookingByIdQueryHandler(
            IBookingRepository bookingRepository,
            IMapper<Booking, GetBookingByIdResponse> mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<GetBookingByIdResponse?> Handle(
            GetBookingByIdQuery request,
            CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.Id, cancellationToken);

            return booking != null ? _mapper.Map(booking, new GetBookingByIdResponse()) : null;
        }
    }
}