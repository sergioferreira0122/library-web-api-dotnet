using FluentAssertions;
using Library.Application.Abstractions;
using Library.Application.Features.Clients;
using Library.Application.Features.Clients.Commands;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.ClientTests
{
    public class UpdateClientFeatureTest
    {
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IValidator<UpdateClientCommand> _validator;
        private readonly IMapper<UpdateClientCommand, Client> _mapper;

        public UpdateClientFeatureTest()
        {
            _clientRepository = new Mock<IClientRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _validator = new UpdateClientValidator();
            _mapper = new UpdateClientMapper();
        }

        [Fact]
        public async Task HandleShouldReturnSuccess()
        {
            //Arrange
            var command = new UpdateClientCommand { Address = "Address", Id = 1, Name = "Name", PhoneNumber = "9394930491"};

            var handler = new UpdateClientCommandHandler(_validator, _clientRepository.Object, _unitOfWork.Object, _mapper);

            var clientFromRepository = new Client
            {
                Address = "Antigo address",
                Id = 1,
                Name = "Antigo name",
                PhoneNumber = "9300000000"
            };

            _clientRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientFromRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().Be(true);
        }

        [Fact]
        public async Task HandleShouldReturnFailureClientNotFound()
        {
            //Arrange
            var command = new UpdateClientCommand { Address = "Address", Id = 1, Name = "Name", PhoneNumber = "9394930491" };

            var handler = new UpdateClientCommandHandler(_validator, _clientRepository.Object, _unitOfWork.Object, _mapper);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(ClientErrors.ClientNotFound);
        }

        [Fact]
        public async Task HandleShouldReturnFailureWhenPhoneNumberHasLetters()
        {
            //Arrange
            var command = new UpdateClientCommand { Address = "Address", Id = 1, Name = "Name", PhoneNumber = "93949dad91" };

            var handler = new UpdateClientCommandHandler(_validator, _clientRepository.Object, _unitOfWork.Object, _mapper);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(ClientErrors.PhoneNumberContainLetters);
        }

        [Fact]
        public void MapShouldReturnSuccess()
        {
            //Arrange
            var updateClientCommand = new UpdateClientCommand { Address = "Novo Address", Id = 1, Name = "Novo Name", PhoneNumber = "939404000" };

            var expectedClient = new Client { Address = "Novo Address", Id = 1, Name = "Novo Name", PhoneNumber = "939404000" };

            var clientFromRepository = new Client { Address = "Antigo Address", Id = 1, Name = "Antigo Name", PhoneNumber = "910000000" };

            //Act
            var clientMapped = _mapper.Map(updateClientCommand, clientFromRepository);

            //Assert
            clientMapped.Should().BeEquivalentTo(expectedClient);
        }
    }
}
