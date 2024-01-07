using FluentAssertions;
using Library.Application.Features.Bookings;
using Library.Application.Features.Bookings.Commands;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.BookingTests
{
    public class DeleteBookingFeatureTest
    {
        private readonly Mock<IBookingRepository> _bookingRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public DeleteBookingFeatureTest()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task HandleShouldReturnSuccess()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new DeleteBookingCommand(1);

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

            _bookingRepository.Setup(
                x => x.DeleteBookingAsync(
                    It.IsAny<Booking>()))
                .Returns(Task.CompletedTask);

            var handler = new DeleteBookingCommandHandler(_bookingRepository.Object, _unitOfWork.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task HandleShouldReturnFailureBookingNotFound()
        {
            //Arrange
            var command = new DeleteBookingCommand(1);

            var handler = new DeleteBookingCommandHandler(_bookingRepository.Object, _unitOfWork.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(BookingErrors.BookingNotFound);
        }
    }
}