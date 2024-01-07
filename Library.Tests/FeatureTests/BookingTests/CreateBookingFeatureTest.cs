using FluentAssertions;
using Library.Application.Features.Bookings.Commands;
using Library.Application.Features.Books;
using Library.Application.Features.Clients;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.BookingTests
{
    public class CreateBookingFeatureTest
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CreateBookingFeatureTest()
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

            var command = new CreateBookingCommand
            {
                BookId = 1,
                ClientId = 1,
                IssuedDate = DateOnly.FromDateTime(dateTimeNow)
            };

            var bookFromRepository = new Book { Author = "autor", Id = 1, PublishDate = dateTimeNow, Title = "titulo" };

            _bookRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookFromRepository);

            var clientFromRepository = new Client { Id = 1, Address = "address", Name = "name", PhoneNumber = "939404040" };

            _clientRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientFromRepository);

            var handler = new CreateBookingCommandHandler(
                _bookingRepository.Object,
                _unitOfWork.Object,
                _clientRepository.Object,
                _bookRepository.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task HandleShouldFailureBookNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new CreateBookingCommand
            {
                BookId = 1,
                ClientId = 1,
                IssuedDate = DateOnly.FromDateTime(dateTimeNow)
            };

            var clientFromRepository = new Client { Id = 1, Address = "address", Name = "name", PhoneNumber = "939404040" };

            _clientRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientFromRepository);

            var handler = new CreateBookingCommandHandler(
                _bookingRepository.Object,
                _unitOfWork.Object,
                _clientRepository.Object,
                _bookRepository.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(BookErrors.BookNotFound);
        }

        [Fact]
        public async Task HandleShouldFailureClientNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new CreateBookingCommand
            {
                BookId = 1,
                ClientId = 1,
                IssuedDate = DateOnly.FromDateTime(dateTimeNow)
            };

            var bookFromRepository = new Book { Author = "autor", Id = 1, PublishDate = dateTimeNow, Title = "titulo" };

            _bookRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookFromRepository);

            var handler = new CreateBookingCommandHandler(
                _bookingRepository.Object,
                _unitOfWork.Object,
                _clientRepository.Object,
                _bookRepository.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(ClientErrors.ClientNotFound);
        }
    }
}