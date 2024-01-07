using FluentAssertions;
using Library.Application;
using Library.Application.Features.Bookings;
using Library.Application.Features.Bookings.Commands;
using Library.Application.Features.Books;
using Library.Application.Features.Clients;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.BookingsControllerTests
{
    public class UpdateBookingEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BookingsController _controller;

        public UpdateBookingEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BookingsController(_sender.Object);
        }

        [Fact]
        public async Task UpdateBookingShouldReturn201WhenBookingIsUpdated()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var bookingId = 1;

            var command = new UpdateBookingCommand { BookId = 1, BookingId = 1, ClientId = 1, IssuedDate = DateOnly.FromDateTime(dateTimeNow) };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<UpdateBookingCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            //Act
            var result = await _controller.UpdateBooking(bookingId, command, default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task UpdateBookingShouldReturn400WhenIdFromParameterIsNotTheSameBodyId()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var bookingId = 1;

            var command = new UpdateBookingCommand { BookId = 1, BookingId = 2, ClientId = 1, IssuedDate = DateOnly.FromDateTime(dateTimeNow) };

            //Act
            var result = await _controller.UpdateBooking(bookingId, command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(400);
            result!.Value.Should().Be(BookingErrors.UpdateBookingNotSameIdFromBodyAndParameter);
        }

        [Fact]
        public async Task UpdateBookingShouldReturn404WhenBookingIsNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var bookingId = 1;

            var command = new UpdateBookingCommand { BookId = 1, BookingId = 1, ClientId = 1, IssuedDate = DateOnly.FromDateTime(dateTimeNow) };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<UpdateBookingCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(BookingErrors.BookingNotFound);

            //Act
            var result = await _controller.UpdateBooking(bookingId, command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(BookingErrors.BookingNotFound);
        }

        [Fact]
        public async Task UpdateBookingShouldReturn404WhenBookIsNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var bookingId = 1;

            var command = new UpdateBookingCommand { BookId = 1, BookingId = 1, ClientId = 1, IssuedDate = DateOnly.FromDateTime(dateTimeNow) };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<UpdateBookingCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(BookErrors.BookNotFound);

            //Act
            var result = await _controller.UpdateBooking(bookingId, command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(BookErrors.BookNotFound);
        }

        [Fact]
        public async Task UpdateBookingShouldReturn404WhenClientIsNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var bookingId = 1;

            var command = new UpdateBookingCommand { BookId = 1, BookingId = 1, ClientId = 1, IssuedDate = DateOnly.FromDateTime(dateTimeNow) };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<UpdateBookingCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(ClientErrors.ClientNotFound);

            //Act
            var result = await _controller.UpdateBooking(bookingId, command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(ClientErrors.ClientNotFound);
        }
    }
}