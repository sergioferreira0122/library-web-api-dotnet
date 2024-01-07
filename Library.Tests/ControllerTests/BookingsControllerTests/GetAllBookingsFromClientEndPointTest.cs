using FluentAssertions;
using Library.Application.Features.Bookings.Queries;
using Library.Application.Features.Clients;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.BookingsControllerTests
{
    public class GetAllBookingsFromClientEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BookingsController _controller;

        public GetAllBookingsFromClientEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BookingsController(_sender.Object);
        }

        [Fact]
        public async Task GetAllBookingsFromClientEndPointShouldReturn200WhenListIsNotEmpty()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var clientId = 1;

            var bookingResponse1 = new GetAllBookingsFromClientResponse
            {
                BookAuthor = "Autor",
                BookTitle = "Title",
                IssueDate = DateOnly.FromDateTime(dateTimeNow)
            };

            var bookingResponse2 = new GetAllBookingsFromClientResponse
            {
                BookAuthor = "Autor 2",
                BookTitle = "Title 2",
                IssueDate = DateOnly.FromDateTime(dateTimeNow)
            };

            var list = new List<GetAllBookingsFromClientResponse> { bookingResponse1, bookingResponse2 };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetAllBookingsFromClientQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            //Act
            var result = await _controller.GetAllBookingsFromClient(clientId, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(200);
            result!.Value.Should().Be(list);
        }

        [Fact]
        public async Task GetAllBookingsFromClientEndPointShouldReturn204WhenListIsEmpty()
        {
            //Arrange
            var list = new List<GetAllBookingsFromClientResponse>();

            var clientId = 1;

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetAllBookingsFromClientQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            //Act
            var result = await _controller.GetAllBookingsFromClient(clientId, default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task GetAllBookingsFromClientEndPointShouldReturn404WhenClientIsNotFound()
        {
            //Arrange
            var clientId = 1;

            //Act
            var result = await _controller.GetAllBookingsFromClient(clientId, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(ClientErrors.ClientNotFound);
        }
    }
}