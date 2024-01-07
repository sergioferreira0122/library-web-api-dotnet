using FluentAssertions;
using Library.Application.Features.Bookings.Queries;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            var dateTimeNow = DateTime.Now.Date;

            var bookingResponse1 = new GetAllBookingsResponse
            {
                BookTitle = "Titulo",
                ClientName = "Client",
                Id = 1,
                IssuedDate = DateOnly.FromDateTime(DateTime.Now.Date)
            };

            var bookingResponse2 = new GetAllBookingsResponse
            {
                BookTitle = "Titulo 2",
                ClientName = "Client 2",
                Id = 2,
                IssuedDate = DateOnly.FromDateTime(DateTime.Now.Date)
            };

            var list = new List<GetAllBookingsResponse> { bookingResponse1, bookingResponse2 };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetAllBookingsQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            //Act
            var result = await _controller.GetAllBookings(default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(200);
            result!.Value.Should().Be(list);
        }

        [Fact]
        public async Task GetAllBookingsEndPointShouldReturn204WhenListIsEmpty()
        {
            //Arrange
            var list = new List<GetAllBookingsResponse>();

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetAllBookingsQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            //Act
            var result = await _controller.GetAllBookings(default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(204);
        }
    }
}