using FluentAssertions;
using Library.Application.Abstractions;
using Library.Application.Features.Bookings.Queries;
using Library.Application.Features.Books.Queries;
using Library.Application.Features.Queries;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Moq;

namespace Library.Tests.BookingTests
{
    public class GetAllBookingsFeatureTest
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly IMapper<ICollection<Booking>, ICollection<GetAllBookingsResponse>> _mapper;

        public GetAllBookingsFeatureTest()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _mapper = new GetAllBookingsMapper();
        }

        [Fact]
        public async Task HandleShouldReturnBookingList()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var query = new GetAllBookingsQuery();

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

            var expected1 = new GetBookingByIdResponse
            {
                BookTitle = "Titulo 1",
                ClientName = "Test 1",
                Id = 1,
                IssuedDate = DateOnly.FromDateTime(dateTimeNow),
            };

            var expected2 = new GetBookingByIdResponse
            {
                BookTitle = "Titulo 2",
                ClientName = "Test 2",
                Id = 2,
                IssuedDate = DateOnly.FromDateTime(dateTimeNow),
            };

            List<Booking> list = new List<Booking> { bookingFromRepository1, bookingFromRepository2 };

            _bookingRepository.Setup(
                x => x.GetBookingsAsync(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            var handler = new GetAllBookingsQueryHandler(_bookingRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task HandleShouldReturnFailureEmptyList()
        {
            //Arrange
            var query = new GetAllBookingsQuery();

            _bookingRepository.Setup(
            x => x.GetBookingsAsync(
            It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Booking>());

            var handler = new GetAllBookingsQueryHandler(_bookingRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void MapShouldReturnSuccess()
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

            var expected1 = new GetBookingByIdResponse
            {
                BookTitle = "Titulo 1",
                ClientName = "Test 1",
                Id = 1,
                IssuedDate = DateOnly.FromDateTime(dateTimeNow),
            };

            var expected2 = new GetBookingByIdResponse
            {
                BookTitle = "Titulo 2",
                ClientName = "Test 2",
                Id = 2,
                IssuedDate = DateOnly.FromDateTime(dateTimeNow),
            };

            List<Booking> list = new List<Booking> { bookingFromRepository1, bookingFromRepository2 };

            _bookingRepository.Setup(
                x => x.GetBookingsAsync(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            //Act
            var clientListMaped = _mapper.Map(list, new List<GetAllBookingsResponse>());

            //Assert
            clientListMaped.ElementAt(0).Should().BeEquivalentTo(expected1);
            clientListMaped.ElementAt(1).Should().BeEquivalentTo(expected2);
        }
    }
}
