using FluentAssertions;
using Library.Application.Features.Bookings.Queries;
using Library.Application.Features.Books.Queries;
using Library.Presentation.Controllers;
using Library.Tests.FeatureTests.BookingTests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.BookingsControllerTests
{
    public class GetBookingByIdEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BookingsController _controller;

        public GetBookingByIdEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BookingsController(_sender.Object);
        }

        [Fact]
        public async Task GetBookingByIdShouldReturn200WhenBookingIsFound()
        {
            /*
            //Arrange
            var bookingId = 1;

            var bookingResponse = new GetBookingByIdResponse
            {
                BookTitle = "Title",
                ClientName = "client",
                Id = 1,
                IssuedDate = DateOnly.FromDateTime(DateTime.Now.Date),
            };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetBookingByIdQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookingResponse);

            //Act
            var result = await _controller.GetBookingById(bookingId, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(200);
            result!.Value.Should().Be(bookingResponse);
             */
        }
    }
}
