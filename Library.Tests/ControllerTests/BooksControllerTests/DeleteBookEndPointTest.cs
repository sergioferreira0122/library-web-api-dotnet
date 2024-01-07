using FluentAssertions;
using Library.Application;
using Library.Application.Features.Books;
using Library.Application.Features.Books.Commands;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.BooksControllerTests
{
    public class DeleteBookEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BooksController _controller;

        public DeleteBookEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BooksController(_sender.Object);
        }

        [Fact]
        public async Task DeleteBookShouldReturn204WhenBookIsDeleted()
        {
            //Arrange
            var bookId = 1;

            _sender.Setup(
                x => x.Send(
                    It.IsAny<DeleteBookCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            //Act
            var result = await _controller.DeleteBook(bookId, default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteBookShouldReturn404WhenNotFound()
        {
            //Arrange
            var bookId = 1;

            _sender.Setup(
                x => x.Send(
                    It.IsAny<DeleteBookCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(BookErrors.BookNotFound);

            //Act
            var result = await _controller.DeleteBook(bookId, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(BookErrors.BookNotFound);
        }
    }
}
