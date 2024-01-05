using FluentAssertions;
using Library.Application.Abstractions;
using Library.Application.Features.Clients.Commands;
using Library.Application.Features.Clients.Queries;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.ClientTests
{
    public class GetClientByIdFeatureTest
    {
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly IMapper<Client, GetClientByIdResponse> _mapper;

        public GetClientByIdFeatureTest()
        {
            _clientRepository = new Mock<IClientRepository>();
            _mapper = new GetClientByIdMapper();
        }

        [Fact]
        public async Task HandleShouldReturnClient()
        {
            //Arrange
            var query = new GetClientByIdQuery { Id = 1 };

            var handler = new GetClientByIdQueryHandler(_clientRepository.Object, _mapper);

            var expected = new GetClientByIdResponse
            {
                Id = 1,
                Address = "Rua nova",
                Name = "Nome",
                PhoneNumber = "9234567890",
            };

            var clientFromRepository = new Client
            {
                Id = 1,
                Address = "Rua nova",
                Name = "Nome",
                PhoneNumber = "9234567890",
            };

            _clientRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientFromRepository);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task HandleShouldReturnFailureClientNotFound()
        {
            //Arrange
            var query = new GetClientByIdQuery { Id = 1 };

            var handler = new GetClientByIdQueryHandler(_clientRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public void MapShouldReturnSuccess()
        {
            //Arrange
            var client = new Client
            {
                Id = 1,
                Address = "Rua map",
                Name = "Manuel",
                PhoneNumber = "939414412",
            };

            var expected = new GetClientByIdResponse
            {
                Id = 1,
                Address = "Rua map",
                Name = "Manuel",
                PhoneNumber = "939414412",
            };

            //Act
            var clientMapped = _mapper.Map(client, new GetClientByIdResponse());

            //Assert
            clientMapped.Should().BeEquivalentTo(expected);
        }
    }
}
