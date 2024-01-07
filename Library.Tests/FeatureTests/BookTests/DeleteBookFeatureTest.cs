using FluentAssertions;
using Library.Application.Features.Books;
using Library.Application.Features.Books.Commands;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.BookTests
{
    public class DeleteBookFeatureTest
    {
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public DeleteBookFeatureTest()
        {
            _bookRepository = new Mock<IBookRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task HandleShouldReturnSuccess()
        {
            //Arrange
            var command = new DeleteBookCommand(1);

            var bookFromRepository = new Book
            {
                Id = 1,
                Author = "Manuel",
                PublishDate = DateTime.Now.Date,
                Title = "Titulo",
            };

            _bookRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookFromRepository);

            var handler = new DeleteBookCommandHandler(_bookRepository.Object, _unitOfWork.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task HandleShouldReturnFailureBookNotFound()
        {
            //Arrange
            var command = new DeleteBookCommand(1);

            var handler = new DeleteBookCommandHandler(_bookRepository.Object, _unitOfWork.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(BookErrors.BookNotFound);
        }
    }
}