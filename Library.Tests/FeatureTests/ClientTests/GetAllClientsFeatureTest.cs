using FluentAssertions;
using Library.Application.Abstractions;
using Library.Application.Features.Clients.Queries;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.ClientTests
{
    public class GetAllClientsFeatureTest
    {
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly IMapper<ICollection<Client>, ICollection<GetAllClientsResponse>> _mapper;

        public GetAllClientsFeatureTest()
        {
            _clientRepository = new Mock<IClientRepository>();
            _mapper = new GetAllClientsMapper();
        }

        [Fact]
        public async Task HandleShouldReturnClientList()
        {
            //Arrange
            var query = new GetAllClientsQuery();

            var handler = new GetAllClientsQueryHandler(_clientRepository.Object, _mapper);

            var client1 = new Client
            {
                Id = 2,
                Address = "Rua nova",
                Name = "Nome",
                PhoneNumber = "9234567890",
            };

            var client2 = new Client
            {
                Id = 1,
                Address = "Rua nova",
                Name = "Nome",
                PhoneNumber = "9234567890",
            };

            List<Client> listFromRepository = new List<Client> { client1, client2 };

            _clientRepository.Setup(
                x => x.GetClientsAsync(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(listFromRepository);

            //Act
            var result = await handler.Handle(query, default);

            //Asssert
            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task HandleShouldReturnFailureEmptyList()
        {
            //Arrange
            var query = new GetAllClientsQuery();

            var handler = new GetAllClientsQueryHandler(_clientRepository.Object, _mapper);

            _clientRepository.Setup(
                x => x.GetClientsAsync(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Client>());

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void MapShouldReturnSuccess()
        {
            //Arrange
            var client1 = new Client
            {
                Id = 1,
                Address = "Rua 1",
                Name = "Manuel",
                PhoneNumber = "939414412",
            };

            var expected1 = new GetClientByIdResponse
            {
                Id = 1,
                Address = "Rua 1",
                Name = "Manuel",
                PhoneNumber = "939414412",
            };

            var client2 = new Client
            {
                Id = 2,
                Address = "Rua 2",
                Name = "Manuel",
                PhoneNumber = "939414412",
            };

            var expected2 = new GetClientByIdResponse
            {
                Id = 2,
                Address = "Rua 2",
                Name = "Manuel",
                PhoneNumber = "939414412",
            };

            List<Client> list = new List<Client> { client1, client2 };

            //Act
            var clientListMaped = _mapper.Map(list, new List<GetAllClientsResponse>());

            //Assert
            clientListMaped.ElementAt(0).Should().BeEquivalentTo(expected1);
            clientListMaped.ElementAt(1).Should().BeEquivalentTo(expected2);
        }
    }
}