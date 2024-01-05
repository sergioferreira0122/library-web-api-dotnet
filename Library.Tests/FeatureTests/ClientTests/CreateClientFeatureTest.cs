using FluentAssertions;
using Library.Application;
using Library.Application.Abstractions;
using Library.Application.Features.Clients;
using Library.Application.Features.Clients.Commands;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.ClientTests
{
    public class CreateClientFeatureTest
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IValidator<CreateClientCommand> _validator;
        private readonly IMapper<CreateClientCommand, Client> _mapper;

        public CreateClientFeatureTest()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validator = new CreateClientValidator();
            _mapper = new CreateClientMapper();
        }

        [Fact]
        public async Task HandleShouldReturnSuccess()
        {
            //Arrange
            var command = new CreateClientCommand { Address = "Rua Nova, 16", Name = "Antonio", PhoneNumber = "939403048" };

            _clientRepositoryMock.Setup(
                 x => x.AddClientAsync(
                     It.IsAny<Client>(),
                     It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            var handler = new CreateClientCommandHandler(
                _validator,
                _clientRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().Be(true);
        }

        [Fact]
        public async Task HandleShouldReturnFailureWhenPhoneNumberHasLetters()
        {
            //Arrange
            var command = new CreateClientCommand { Address = "Rua Nova, 16", Name = "Antonio", PhoneNumber = "93940da4144" };

            var handler = new CreateClientCommandHandler(
                _validator,
                _clientRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(ClientErrors.PhoneNumberContainLetters);
        }

        [Fact]
        public void MapShouldReturnSuccess()
        {
            //Arrange
            var createClientCommand = new CreateClientCommand
            {
                Address = "Rua atualizada",
                Name = "Manuel",
                PhoneNumber = "939414412",
            };

            var expectedClient = new Client
            {
                Address = "Rua atualizada",
                Name = "Manuel",
                PhoneNumber = "939414412",

            };

            //Act
            var clientMapped = _mapper.Map(createClientCommand, new Client());

            //Assert
            clientMapped.Should().BeEquivalentTo(expectedClient);
        }
    }
}
