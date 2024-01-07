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
    public class UpdateBookEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BooksController _controller;

        public UpdateBookEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BooksController(_sender.Object);
        }

        [Fact]
        public async Task UpdateBookEndPointShouldReturn201WhenBookIsUpdated()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var bookId = 1;

            var command = new UpdateBookCommand
            {
                Author = "Autor",
                Id = 1,
                PublishDate = DateOnly.FromDateTime(dateTimeNow),
                Title = "Title"
            };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<UpdateBookCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            //Act
            var result = await _controller.UpdateBook(bookId, command, default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task UpdateBookEndPointShouldReturn404WhenBookIsNotFound()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var bookId = 1;

            var command = new UpdateBookCommand
            {
                Author = "Autor",
                Id = 1,
                PublishDate = DateOnly.FromDateTime(dateTimeNow),
                Title = "Title"
            };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<UpdateBookCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(BookErrors.BookNotFound);

            //Act
            var result = await _controller.UpdateBook(bookId, command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(404);
            result!.Value.Should().Be(BookErrors.BookNotFound);
        }

        [Fact]
        public async Task UpdateBookEndPointShouldReturn400WhenIdFromParameterIsNotTheSameBodyId()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var bookId = 2;

            var command = new UpdateBookCommand
            {
                Author = "Autor",
                Id = 1,
                PublishDate = DateOnly.FromDateTime(dateTimeNow),
                Title = "Title"
            };

            //Act
            var result = await _controller.UpdateBook(bookId, command, default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(400);
            result!.Value.Should().Be(BookErrors.UpdateBookNotSameIdFromBodyAndParameter);
        }
    }
}