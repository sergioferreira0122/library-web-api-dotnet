using FluentAssertions;
using Library.Application.Abstractions;
using Library.Application.Features.Bookings.Queries;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.BookingTests
{
    public class GetAllBookingsFromClientFeatureTest
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly IMapper<ICollection<Booking>, ICollection<GetAllBookingsFromClientResponse>> _mapper;

        public GetAllBookingsFromClientFeatureTest()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _clientRepository = new Mock<IClientRepository>();
            _mapper = new GetAllBookingsFromClientMapper();
        }

        [Fact]
        public async Task HandleShouldReturnBookingListFromClient()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var query = new GetAllBookingsFromClientQuery(1);

            var client = new Client
            {
                Address = "Address",
                Id = 1,
                Name = "Test",
                PhoneNumber = "9234567890",
            };

            _clientRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(client);

            var book1 = new Book
            {
                Author = "Autor",
                Id = 1,
                PublishDate = dateTimeNow,
                Title = "Titulo 1",
            };

            var bookingFromRepository1 = new Booking
            {
                Id = 1,
                Book = book1,
                Client = client,
                IssueDate = dateTimeNow,
            };

            var book2 = new Book
            {
                Author = "Autor",
                Id = 1,
                PublishDate = dateTimeNow,
                Title = "Titulo 2",
            };
            var bookingFromRepository2 = new Booking
            {
                Id = 2,
                Book = book2,
                Client = client,
                IssueDate = dateTimeNow,
            };

            List<Booking> list = new List<Booking> { bookingFromRepository1, bookingFromRepository2 };  

            _bookingRepository.Setup(
                x => x.GetBookingsFromClientIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            var handler = new GetAllBookingsFromClientQueryHandler(_bookingRepository.Object, _clientRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().HaveCountGreaterThan(1);
        }

        [Fact]
        public async Task HandleShouldReturnFailureEmptyList()
        {
            //Arrange
            var query = new GetAllBookingsFromClientQuery(1);

            var client = new Client
            {
                Address = "Address",
                Id = 1,
                Name = "Test",
                PhoneNumber = "9234567890",
            };

            _clientRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(client);

            _bookingRepository.Setup(
            x => x.GetBookingsFromClientIdAsync(
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Booking>());

            var handler = new GetAllBookingsFromClientQueryHandler(_bookingRepository.Object, _clientRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task HandleShouldReturnFailureClientNotFound()
        {
            //Arrange
            var query = new GetAllBookingsFromClientQuery(1);

            _bookingRepository.Setup(
            x => x.GetBookingsFromClientIdAsync(
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Booking>());

            var handler = new GetAllBookingsFromClientQueryHandler(_bookingRepository.Object, _clientRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public void MapShouldReturnSucess()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var book1 = new Book
            {
                Author = "Autor",
                Id = 1,
                PublishDate = dateTimeNow,
                Title = "Titulo 1",
            };

            var client1 = new Client
            {
                Id = 1,
                Address = "Endereco",
                Name = "Test 1",
                PhoneNumber = "9234567890",
            };

            var bookingFromRepository1 = new Booking
            {
                Id = 1,
                Book = book1,
                Client = client1,
                IssueDate = dateTimeNow,
            };

            var book2 = new Book
            {
                Author = "Autor",
                Id = 1,
                PublishDate = dateTimeNow,
                Title = "Titulo 2",
            };

            var client2 = new Client
            {
                Id = 1,
                Address = "Endereco",
                Name = "Test 2",
                PhoneNumber = "9234567890",
            };

            var bookingFromRepository2 = new Booking
            {
                Id = 2,
                Book = book2,
                Client = client2,
                IssueDate = dateTimeNow,
            };

            var expected1 = new GetAllBookingsFromClientResponse
            {
                BookTitle = "Titulo 1",
                BookAuthor = "Autor",
                IssueDate = DateOnly.FromDateTime(dateTimeNow),
            };

            var expected2 = new GetAllBookingsFromClientResponse
            {
                BookTitle = "Titulo 2",
                BookAuthor = "Autor",
                IssueDate = DateOnly.FromDateTime(dateTimeNow),
            };

            List<Booking> list = new List<Booking> { bookingFromRepository1, bookingFromRepository2 };
            
            //Act
            var clientListMaped = _mapper.Map(list, new List<GetAllBookingsFromClientResponse>());

            //Assert
            clientListMaped.ElementAt(0).Should().BeEquivalentTo(expected1);
            clientListMaped.ElementAt(1).Should().BeEquivalentTo(expected2);
        }
    }
}
