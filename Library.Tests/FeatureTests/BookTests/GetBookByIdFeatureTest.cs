using FluentAssertions;
using Library.Application.Abstractions;
using Library.Application.Features.Books.Queries;
using Library.Application.Features.Clients.Queries;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.BookTests
{
    public class GetBookByIdFeatureTest
    {
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly IMapper<Book, GetBookByIdResponse> _mapper;

        public GetBookByIdFeatureTest()
        {
            _bookRepository = new Mock<IBookRepository>();
            _mapper = new GetBookByIdMapper();
        }

        [Fact]
        public async Task HandleShouldReturnBook()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;
            var query = new GetBookByIdQuery { Id = 1 };

            var bookFromRepository = new Book
            {
                Author = "Author",
                Id = 1,
                PublishDate = dateTimeNow,
                Title = "Title",
            };

            var expected = new GetBookByIdResponse
            {
                Author = "Author",
                Id = 1,
                PublishDate = DateOnly.FromDateTime(dateTimeNow),
                Title = "Title",
            };

            _bookRepository.Setup(
                x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookFromRepository);

            var handler = new GetBookByIdQueryHandler(_bookRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task HandleShouldReturnFailureBookNotFound()
        {
            //Arrange
            var query = new GetBookByIdQuery { Id = 1 };

            var handler = new GetBookByIdQueryHandler(_bookRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public void MapShouldReturnSuccess()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var book = new Book
            {
                Id = 1,
                Title = "Title",
                Author = "Author",
                PublishDate = dateTimeNow,
            };

            var expected = new GetBookByIdResponse
            {
                Id = 1,
                Author = "Author",
                Title = "Title",
                PublishDate = DateOnly.FromDateTime(dateTimeNow),
            };

            //Act
            var bookMapped = _mapper.Map(book, new GetBookByIdResponse());

            //Assert
            bookMapped.Should().BeEquivalentTo(expected);
        }
    }
}
