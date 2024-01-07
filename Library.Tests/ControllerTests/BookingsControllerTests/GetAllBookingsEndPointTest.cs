using Library.Presentation.Controllers;
using MediatR;
using Moq;

namespace Library.Tests.ControllerTests.BookingsControllerTests
{
    public class GetAllBookingsEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BookingsController _controller;

        public GetAllBookingsEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BookingsController(_sender.Object);
        }

        [Fact]
        public async Task GetAllBookingsEndPointShouldReturn200WhenListIsNotEmpty()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
