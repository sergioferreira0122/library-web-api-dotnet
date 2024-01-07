using FluentAssertions;
using Library.Application.Features.Clients;
using Library.Application.Features.Clients.Queries;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.ClientsControllerTests
{
    public class GetClientByIdEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly ClientsController _controller;

        public GetClientByIdEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new ClientsController(_sender.Object);
        }

        [Fact]
        public async Task GetClientByIdEndPointShouldReturn200WhenClientIsFound()
        {
            //Arrange
            int clientId = 1;

            var clientResponse = new GetClientByIdResponse
            {
                Address = "Address",
                Id = 1,
                Name = "Test",
                PhoneNumber = "123919293"
            };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetClientByIdQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientResponse);

            //Act
            var result = await _controller.GetClientById(clientId, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(200);
            result!.Value.Should().Be(clientResponse);
        }

        [Fact]
        public async Task GetClientByIdEndPointShouldReturn404WhenClientNotFound()
        {
            //Arrange
            int clientId = 1;

            //Act
            var result = await _controller.GetClientById(clientId, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(ClientErrors.ClientNotFound);
        }
    }
}
