using FluentAssertions;
using Library.Application.Features.Clients.Queries;
using Library.Domain.Entities;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.ClientsControllerTests
{
    public class GetAllClientsEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly ClientsController _controller;

        public GetAllClientsEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new ClientsController(_sender.Object);
        }

        [Fact]
        public async Task GetAllClientsEndPointShouldReturn200WhenListIsNotEmpty()
        {
            //Arrange
            var query = new GetAllClientsQuery();

            var client1 = new GetAllClientsResponse
            {
                Id = 2,
                Address = "Rua nova",
                Name = "Nome",
                PhoneNumber = "9234567890",
            };

            var client2 = new GetAllClientsResponse
            {
                Id = 1,
                Address = "Rua nova",
                Name = "Nome",
                PhoneNumber = "9234567890",
            };

            List<GetAllClientsResponse> list = new List<GetAllClientsResponse> { client1, client2 };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetAllClientsQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            //Act
            var result = await _controller.GetAllClients(default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(200);
            result!.Value.Should().Be(list);
        }

        [Fact]
        public async Task GetAllClientsEndPointShouldReturn204WhenListIsEmpty()
        {
            //Arrange
            var query = new GetAllClientsQuery();

            var list = new List<GetAllClientsResponse>();

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetAllClientsQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            //Act
            var result = await _controller.GetAllClients(default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(204);
        }
    }
}
