using Library.Application.Abstractions;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Bookings.Queries
{
    public class GetAllBookingsFromClientQueryHandler
        : IQueryHandler<GetAllBookingsFromClientQuery, ICollection<GetAllBookingsFromClientResponse>?>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper<ICollection<Booking>, ICollection<GetAllBookingsFromClientResponse>> _mapper;

        public GetAllBookingsFromClientQueryHandler(
            IBookingRepository bookingRepository,
            IClientRepository clientRepository,
            IMapper<ICollection<Booking>, ICollection<GetAllBookingsFromClientResponse>> mapper)
        {
            _bookingRepository = bookingRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllBookingsFromClientResponse>?> Handle(
            GetAllBookingsFromClientQuery request,
            CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.clientId, cancellationToken);
            if (client == null)
                return null;
            
            var bookings = await _bookingRepository.GetBookingsFromClientIdAsync(request.clientId, cancellationToken);

            return _mapper.Map(bookings, new List<GetAllBookingsFromClientResponse>());
        }
    }
}
