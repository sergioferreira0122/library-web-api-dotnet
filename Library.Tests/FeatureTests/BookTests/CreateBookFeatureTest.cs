using FluentAssertions;
using Library.Application.Abstractions;
using Library.Application.Features.Books.Commands;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.BookTests
{
    public class CreateBookFeatureTest
    {
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper<CreateBookCommand, Book> _mapper;

        public CreateBookFeatureTest()
        {
            _bookRepository = new Mock<IBookRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new CreateBookMapper();
        }

        [Fact]
        public async Task HandlehouldReturnSuccess()
        {
            //Arrange
            var command = new CreateBookCommand
            {
                Author = "Shakes",
                PublishDate = DateOnly.FromDateTime(new DateTime()),
                Title = "Titulo",
            };

            var handler = new CreateBookCommandHandler(_bookRepository.Object, _unitOfWork.Object, _mapper);

            _bookRepository.Setup(
                x => x.AddBookAsync(
                    It.IsAny<Book>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void MapShouldReturnSuccess()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var bookCommand = new CreateBookCommand
            {
                Author = "Manuel",
                PublishDate = DateOnly.FromDateTime(dateTimeNow),
                Title = "Titulo"
            };

            var expectedBook = new Book
            {
                Author = "Manuel",
                PublishDate = dateTimeNow,
                Title = "Titulo",
            };

            //Act
            var bookMapped = _mapper.Map(bookCommand, new Book());

            //Assert
            bookMapped.Should().BeEquivalentTo(expectedBook);
        }
    }
}