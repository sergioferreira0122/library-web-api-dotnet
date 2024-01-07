using FluentAssertions;
using Library.Application.Features.Books;
using Library.Application.Features.Books.Queries;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.BooksControllerTests
{
    public class GetBookByIdEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BooksController _controller;

        public GetBookByIdEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BooksController(_sender.Object);
        }

        [Fact]
        public async Task GetBookByIdEndPointShouldReturn200WhenBookIsFound()
        {
            //Arrange
            var bookId = 1;

            var bookResponse = new GetBookByIdResponse
            {
                Author = "Autor",
                Id = bookId,
                PublishDate = DateOnly.FromDateTime(DateTime.Now.Date),
                Title = "Titulo"
            };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetBookByIdQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookResponse);

            //Act
            var result = await _controller.GetBookById(bookId, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(200);
            result!.Value.Should().Be(bookResponse);
        }

        [Fact]
        public async Task GetBookByIdEndPointShouldReturn404WhenBookNotFound()
        {
            //Arrange
            int bookId = 1;

            //Act
            var result = await _controller.GetBookById(bookId, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(BookErrors.BookNotFound);
        }
    }
}