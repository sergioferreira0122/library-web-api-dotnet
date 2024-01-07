using FluentAssertions;
using Library.Application.Features.Clients;
using Library.Application.Features.Clients.Commands;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.ClientTests
{
    public class DeleteClientFeatureTest
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteClientFeatureTest()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task HandleShouldReturnFailureClientNotFound()
        {
            //Arrange
            var command = new DeleteClientCommand(1);

            var handler = new DeleteClientCommandHandler(_clientRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(ClientErrors.ClientNotFound);
        }

        [Fact]
        public async Task HandleShouldReturnSuccess()
        {
            //Arrange
            var command = new DeleteClientCommand(1);

            var client = new Client { Id = 1, Address = "Rua antiga", Name = "Ulysses", PhoneNumber = "9399393939" };

            _clientRepositoryMock.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(client);

            _clientRepositoryMock.Setup(
                x => x.DeleteClientAsync(
                    It.IsAny<Client>()))
                .Returns(Task.CompletedTask);

            var handler = new DeleteClientCommandHandler(_clientRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().Be(true);
        }
    }
}