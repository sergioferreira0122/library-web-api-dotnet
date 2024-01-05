using FluentAssertions;
using Library.Application.Abstractions;
using Library.Application.Features.Bookings.Queries;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.BookingTests
{
    public class GetBookingByIdFeatureTest
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly IMapper<Booking, GetBookingByIdResponse> _mapper;

        public GetBookingByIdFeatureTest()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _mapper = new GetBookingByIdMapper();
        }

        [Fact]
        public async Task HandleShouldReturnBooking()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var query = new GetBookingByIdQuery(1);

            var expected = new GetBookingByIdResponse
            {
                BookTitle = "Titulo",
                ClientName = "Test",
                Id = 1,
                IssuedDate = DateOnly.FromDateTime(dateTimeNow),
            };

            var book = new Book
            {
                Author = "Autor",
                Id = 1,
                PublishDate = dateTimeNow,
                Title = "Titulo",
            };

            var client = new Client
            {
                Id = 1,
                Address = "Endereco",
                Name = "Test",
                PhoneNumber = "9234567890",
            };

            var bookingFromRepository = new Booking
            {
                Id = 1,
                Book = book,
                Client = client,
                IssueDate = dateTimeNow,
            };

            _bookingRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookingFromRepository);

            var handler = new GetBookingByIdQueryHandler(_bookingRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task HandleShouldReturnFailureBookingNotFound()
        {
            //Arrange
            var query = new GetBookingByIdQuery(1);

            var handler = new GetBookingByIdQueryHandler(_bookingRepository.Object, _mapper);

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

            var book = new Book
            {
                Author = "Autor",
                Id = 1,
                PublishDate = dateTimeNow,
                Title = "Titulo",
            };

            var client = new Client
            {
                Id = 1,
                Address = "Endereco",
                Name = "Test",
                PhoneNumber = "9234567890",
            };

            var booking = new Booking
            {
                Id = 1,
                Book = book,
                Client = client,
                IssueDate = dateTimeNow,
            };

            var expected = new GetBookingByIdResponse
            {
                BookTitle = "Titulo",
                ClientName = "Test",
                Id = 1,
                IssuedDate = DateOnly.FromDateTime(dateTimeNow),
            };

            //Act
            var bookingMapped = _mapper.Map(booking, new GetBookingByIdResponse());

            //Assert
            bookingMapped.Should().BeEquivalentTo(expected);
        }
    }
}
