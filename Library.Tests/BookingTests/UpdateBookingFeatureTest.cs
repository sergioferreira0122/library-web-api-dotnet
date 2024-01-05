using Library.Domain.Abstractions;
using Moq;

namespace Library.Tests.BookingTests
{
    public class UpdateBookingFeatureTest
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public UpdateBookingFeatureTest()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _clientRepository = new Mock<IClientRepository>();
            _bookRepository = new Mock<IBookRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task HandleShouldReturnSuccess()
        {

        }
    }
}
