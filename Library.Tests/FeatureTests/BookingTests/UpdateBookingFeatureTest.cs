using FluentAssertions;
using Library.Application.Features.Bookings;
using Library.Application.Features.Bookings.Commands;
using Library.Application.Features.Books;
using Library.Application.Features.Clients;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.BookingTests
{
    public class UpdateBookingFeatureTest
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public UpdateBookingFeatureTest()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _clientRepository = new Mock<IClientRepository>();
            _bookRepository = new Mock<IBookRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task HandleShouldReturnSuccess()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new UpdateBookingCommand
            {
                BookId = 1,
                BookingId = 1,
                ClientId = 1,
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

            _bookRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            _clientRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(client);

            _bookingRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookingFromRepository);

            var handler = new UpdateBookingCommandHandler(
                _bookingRepository.Object,
                _clientRepository.Object,
                _bookRepository.Object,
                _unitOfWork.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task HandleShouldReturnFailureBookNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new UpdateBookingCommand
            {
                BookId = 1,
                BookingId = 1,
                ClientId = 1,
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

            _clientRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(client);

            _bookingRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookingFromRepository);

            var handler = new UpdateBookingCommandHandler(
                _bookingRepository.Object,
                _clientRepository.Object,
                _bookRepository.Object,
                _unitOfWork.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(BookErrors.BookNotFound);
        }

        [Fact]
        public async Task HandleShouldReturnFailureClientNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new UpdateBookingCommand
            {
                BookId = 1,
                BookingId = 1,
                ClientId = 1,
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

            var handler = new UpdateBookingCommandHandler(
                _bookingRepository.Object,
                _clientRepository.Object,
                _bookRepository.Object,
                _unitOfWork.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(ClientErrors.ClientNotFound);
        }

        [Fact]
        public async Task HandleShouldReturnFailureBookingNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new UpdateBookingCommand
            {
                BookId = 1,
                BookingId = 1,
                ClientId = 1,
                IssuedDate = DateOnly.FromDateTime(dateTimeNow),
            };

            var handler = new UpdateBookingCommandHandler(
                _bookingRepository.Object,
                _clientRepository.Object,
                _bookRepository.Object,
                _unitOfWork.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(BookingErrors.BookingNotFound);
        }
    }
}