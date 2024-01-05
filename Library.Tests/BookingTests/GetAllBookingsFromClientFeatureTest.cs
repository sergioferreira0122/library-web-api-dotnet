using Library.Application.Abstractions;
using Library.Application.Features.Bookings.Queries;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.BookingTests
{
    public class GetAllBookingsFromClientFeatureTest
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly IMapper<ICollection<Booking>, ICollection<GetAllBookingsFromClientResponse>> _mapper;

        public GetAllBookingsFromClientFeatureTest()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _clientRepository = new Mock<IClientRepository>();
            _mapper = new GetAllBookingsFromClientMapper();
        }

        [Fact]
        public async Task HandleShouldReturnBookingListFromClient()
        {
            //Arrange
            var query = new GetAllBookingsFromClientQuery(1);


            //Act

            //Assert
        }
    }
}
