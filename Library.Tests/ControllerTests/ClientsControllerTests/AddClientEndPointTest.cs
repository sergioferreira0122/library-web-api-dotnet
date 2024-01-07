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
    public class AddClientEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly ClientsController _controller;

        public AddClientEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new ClientsController(_sender.Object);
        }

        [Fact]
        public async Task AddClientEndPointShouldReturn201WhenCreated()
        {
            //Arrange
            var command = new CreateClientCommand
            {
                Address = "Address",
                Name = "Name",
                PhoneNumber = "939304040"
            };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<CreateClientCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            //Act
            var result = await _controller.AddClient(command, default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task AddClientEndPointShouldReturn400WhenPhoneNumberContainsLetter()
        {
            //Arrange
            var command = new CreateClientCommand
            {
                Address = "Address",
                Name = "Name",
                PhoneNumber = "dwa1991"
            };

            var resultObj = ClientErrors.PhoneNumberContainLetters;

            _sender.Setup(
                x => x.Send(
                    It.IsAny<CreateClientCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultObj);

            //Act
            var result = await _controller.AddClient(command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(400);
            result!.Value.Should().Be(ClientErrors.PhoneNumberContainLetters);
        }
    }
}