using FluentAssertions;
using Library.Application;
using Library.Application.Features.Books.Commands;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.BooksControllerTests
{
    public class AddBookEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BooksController _controller;

        public AddBookEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BooksController(_sender.Object);
        }

        [Fact]
        public async Task AddBookShouldReturn201WhenBookIsCreated()
        {
            //Arrange
            var dateTimeNow = DateTime.Now.Date;

            var command = new CreateBookCommand { Author = "Autor", Title = "Titulo", PublishDate = DateOnly.FromDateTime(dateTimeNow) };

            _sender.Setup(
                x => x.Send(
                    It.IsAny<CreateBookCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            //Act
            var result = await _controller.AddBook(command, default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(201);
        }
    }
}