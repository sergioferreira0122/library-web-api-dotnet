using FluentAssertions;
using Library.Application;
using Library.Application.Features.Bookings;
using Library.Application.Features.Bookings.Commands;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.BookingsControllerTests
{
    public class DeleteBookingEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BookingsController _controller;

        public DeleteBookingEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BookingsController(_sender.Object);
        }

        [Fact]
        public async Task DeleteBookingShouldReturn204WhenBookingIsDeleted()
        {
            //Arrange
            var bookingId = 1;

            _sender.Setup(
                x => x.Send(
                    It.IsAny<DeleteBookingCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            //Act
            var result = await _controller.DeleteBooking(bookingId, default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteBookingShouldReturn404WhenBookingIsNotFound()
        {
            //Arrange
            var bookingId = 1;

            _sender.Setup(
                x => x.Send(
                    It.IsAny<DeleteBookingCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(BookingErrors.BookingNotFound);

            //Act
            var result = await _controller.DeleteBooking(bookingId, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(BookingErrors.BookingNotFound);
        }
    }
}
