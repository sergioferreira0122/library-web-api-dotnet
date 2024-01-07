using FluentAssertions;
using Library.Application;
using Library.Application.Features.Bookings.Commands;
using Library.Application.Features.Books;
using Library.Application.Features.Clients;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.BookingsControllerTests
{
    public class AddBookingEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BookingsController _controller;

        public AddBookingEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BookingsController(_sender.Object);
        }

        [Fact]
        public async Task AddBookingShouldReturn201WhenBookingIsCreated()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new CreateBookingCommand { BookId = 1, ClientId = 1, IssuedDate = DateOnly.FromDateTime(dateTimeNow) };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<CreateBookingCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            //Act
            var result = await _controller.AddBooking(command, default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task AddBookingShouldReturn404WhenBookIsNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new CreateBookingCommand { BookId = 1, ClientId = 1, IssuedDate = DateOnly.FromDateTime(dateTimeNow) };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<CreateBookingCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(BookErrors.BookNotFound);

            //Act
            var result = await _controller.AddBooking(command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(BookErrors.BookNotFound);
        }

        [Fact]
        public async Task AddBookingShouldReturn404WhenClientIsNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new CreateBookingCommand { BookId = 1, ClientId = 1, IssuedDate = DateOnly.FromDateTime(dateTimeNow) };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<CreateBookingCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(ClientErrors.ClientNotFound);

            //Act
            var result = await _controller.AddBooking(command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(ClientErrors.ClientNotFound);
        }
    }
}