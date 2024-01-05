using FluentAssertions;
using Library.Application.Abstractions;
using Library.Application.Features.Books;
using Library.Application.Features.Books.Commands;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.BookTests
{
    public class UpdateBookFeatureTest
    {
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper<UpdateBookCommand, Book> _mapper;

        public UpdateBookFeatureTest()
        {
            _bookRepository = new Mock<IBookRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new UpdateBookMapper();
        }

        [Fact]
        public async Task HandleShouldReturnSuccess()
        {
            //Arrange
            var dateTimeNow = DateTime.Now;
            var oldDateTime = DateTime.Now.AddMinutes(1);

            var command = new UpdateBookCommand
            {
                Id = 1,
                Author = "Joe",
                PublishDate = DateOnly.FromDateTime(dateTimeNow),
                Title = "Titulo"
            };

            var bookFromRepository = new Book
            {
                Id = 1,
                Title = "Title",
                Author = "Author",
                PublishDate = oldDateTime
            };

            _bookRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookFromRepository);

            var handler = new UpdateBookCommandHandler(_bookRepository.Object, _unitOfWork.Object, _mapper);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task HandleShouldReturnFailureBookNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now;

            var command = new UpdateBookCommand
            {
                Id = 1,
                Author = "Joe",
                PublishDate = DateOnly.FromDateTime(dateTimeNow),
                Title = "Titulo"
            };

            var handler = new UpdateBookCommandHandler(_bookRepository.Object, _unitOfWork.Object, _mapper);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Error.Should().Be(BookErrors.BookNotFound);
        }

        [Fact]
        public void MapShouldReturnSuccess()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;
            var oldDateTime = DateTime.Now.Date.AddDays(1);

            var bookCommand = new UpdateBookCommand
            {
                Author = "Author",
                Id = 1,
                Title = "Title",
                PublishDate = DateOnly.FromDateTime(dateTimeNow)
            };

            var bookFromRepository = new Book
            {
                Id = 1,
                Author = "Antigo autor",
                PublishDate = oldDateTime,
                Title = "Antigo Title",
            };

            var expectedBook = new Book
            {
                Id = 1,
                Title = "Title",
                PublishDate = dateTimeNow,
                Author = "Author",
            };

            //Act
            var bookMapped = _mapper.Map(bookCommand, bookFromRepository);

            //Assert
            bookMapped.Should().BeEquivalentTo(expectedBook);
        }
    }
}
