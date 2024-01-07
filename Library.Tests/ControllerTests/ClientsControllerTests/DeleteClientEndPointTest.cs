using FluentAssertions;
using Library.Application;
using Library.Application.Features.Clients;
using Library.Application.Features.Clients.Commands;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.ClientsControllerTests
{
    public class DeleteClientEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly ClientsController _controller;

        public DeleteClientEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new ClientsController(_sender.Object);
        }

        [Fact]
        public async Task DeleteClientEndPointShouldReturn204WhenClientIsDeleted()
        {
            //Arrange
            var clientId = 1;

            var command = new DeleteClientCommand(1);

            _sender.Setup(
                x => x.Send(
                    It.IsAny<DeleteClientCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            //Act
            var result = await _controller.DeleteClient(clientId, default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteClientEndPointShouldReturn404WhenClientNotFound()
        {
            //Arrange
            var clientId = 1;

            var command = new DeleteClientCommand(1);

            _sender.Setup(
                x => x.Send(
                    It.IsAny<DeleteClientCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(ClientErrors.ClientNotFound);

            //Act
            var result = await _controller.DeleteClient(clientId, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(ClientErrors.ClientNotFound);
        }
    }
}