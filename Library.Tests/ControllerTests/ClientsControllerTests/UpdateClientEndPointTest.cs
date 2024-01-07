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
    public class UpdateClientEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly ClientsController _controller;

        public UpdateClientEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new ClientsController(_sender.Object);
        }

        [Fact]
        public async Task UpdateClientShouldReturn201WhenClientIsUpdated()
        {
            //Arrange
            var clientId = 1;

            var command = new UpdateClientCommand
            {
                Address = "Address",
                Id = 1,
                Name = "Test",
                PhoneNumber = "1234567890",
            };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<UpdateClientCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success);

            //Act
            var result = await _controller.UpdateClient(clientId, command, default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task UpdateClientShouldReturn400WhenIdFromParameterIsNotTheSameInCommand()
        {
            //Arrange
            var clientId = 2;

            var command = new UpdateClientCommand
            {
                Address = "Address",
                Id = 1,
                Name = "Test",
                PhoneNumber = "1234567890",
            };

            //Act
            var result = await _controller.UpdateClient(clientId, command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(400);
            result!.Value.Should().Be(ClientErrors.UpdateClientNotSameIdFromBodyAndParameter);
        }

        [Fact]
        public async Task UpdateClientShouldReturn400WhenPhoneNumberContainsLetters()
        {
            //Arrange
            var clientId = 1;

            var command = new UpdateClientCommand
            {
                Address = "Address",
                Id = 1,
                Name = "Test",
                PhoneNumber = "ad1132123",
            };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<UpdateClientCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(ClientErrors.PhoneNumberContainLetters);

            //Act
            var result = await _controller.UpdateClient(clientId, command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(400);
            result!.Value.Should().Be(ClientErrors.PhoneNumberContainLetters);
        }

        [Fact]
        public async Task UpdateClientShouldReturn404WhenClientIsNotFound()
        {
            //Arrange
            var clientId = 1;

            var command = new UpdateClientCommand
            {
                Address = "Address",
                Id = 1,
                Name = "Test",
                PhoneNumber = "ad1132123",
            };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<UpdateClientCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(ClientErrors.ClientNotFound);

            //Act
            var result = await _controller.UpdateClient(clientId, command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(ClientErrors.ClientNotFound);
        }
    }
}