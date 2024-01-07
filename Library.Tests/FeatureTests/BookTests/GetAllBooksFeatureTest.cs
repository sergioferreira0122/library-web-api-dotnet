using FluentAssertions;
using Library.Application.Abstractions;
using Library.Application.Features.Books.Queries;
using Library.Application.Features.Queries;
using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Moq;

namespace Library.Tests.FeatureTests.BookTests
{
    public class GetAllBooksFeatureTest
    {
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly IMapper<ICollection<Book>, ICollection<GetAllBooksResponse>> _mapper;

        public GetAllBooksFeatureTest()
        {
            _bookRepository = new Mock<IBookRepository>();
            _mapper = new GetAllBooksMapper();
        }

        [Fact]
        public async Task HandleShouldReturnBookList()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;
            var query = new GetAllBooksQuery();

            var client1 = new Book
            {
                Id = 1,
                Author = "Author 1",
                PublishDate = dateTimeNow,
                Title = "Title 1",
            };

            var client2 = new Book
            {
                Id = 2,
                Author = "Author 2",
                PublishDate = dateTimeNow,
                Title = "Title 2",
            };

            List<Book> list = new List<Book> { client1, client2 };

            _bookRepository.Setup(
                x => x.GetBooksAsync(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            var handler = new GetAllBooksQueryHandler(_bookRepository.Object, _mapper);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task HandleShouldReturnFailureEmptyList()
        {
            //Arrange
            var query = new GetAllBooksQuery();

            _bookRepository.Setup(
                x => x.GetBooksAsync(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Book>());

            var handler = new GetAllBooksQueryHandler(_bookRepository.Object, _mapper);

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
                Id = 1,
                Author = "Author 1",
                PublishDate = dateTimeNow,
                Title = "Title 1",
            };

            var expected1 = new GetBookByIdResponse
            {
                Id = 1,
                Author = "Author 1",
                PublishDate = DateOnly.FromDateTime(dateTimeNow),
                Title = "Title 1",
            };

            var book2 = new Book
            {
                Id = 2,
                Author = "Author 2",
                PublishDate = dateTimeNow,
                Title = "Title 2",
            };

            var expected2 = new GetBookByIdResponse
            {
                Id = 2,
                Author = "Author 2",
                PublishDate = DateOnly.FromDateTime(dateTimeNow),
                Title = "Title 2",
            };

            List<Book> list = new List<Book> { book1, book2 };

            //Act
            var clientListMaped = _mapper.Map(list, new List<GetAllBooksResponse>());

            //Assert
            clientListMaped.ElementAt(0).Should().BeEquivalentTo(expected1);
            clientListMaped.ElementAt(1).Should().BeEquivalentTo(expected2);
        }
    }
}