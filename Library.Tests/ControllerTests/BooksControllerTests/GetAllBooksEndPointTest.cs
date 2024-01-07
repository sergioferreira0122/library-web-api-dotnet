using FluentAssertions;
using Library.Application.Features.Books.Queries;
using Library.Application.Features.Clients.Queries;
using Library.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Tests.ControllerTests.BooksControllerTests
{
    public class GetAllBooksEndPointTest
    {
        private readonly Mock<ISender> _sender;
        private readonly BooksController _controller;

        public GetAllBooksEndPointTest()
        {
            _sender = new Mock<ISender>();
            _controller = new BooksController(_sender.Object);
        }

        [Fact]
        public async Task GetAllBooksShouldReturn200WhenListIsNotEmpty()
        {
            //Arrange
            var dateTimeNow = DateOnly.FromDateTime(DateTime.Now.Date);

            var book1 = new GetAllBooksResponse
            {
                Id = 1,
                Author = "Autor",
                PublishDate = dateTimeNow,
                Title = "Titulo"
            };

            var book2 = new GetAllBooksResponse
            {
                Id = 2,
                Author = "Autor",
                PublishDate = dateTimeNow,
                Title = "Titulo"
            };

            List<GetAllBooksResponse> list = new List<GetAllBooksResponse> { book1, book2};

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetAllBooksQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            //Act
            var result = await _controller.GetAllBooks(default) as ObjectResult;

            //Assert
            result!.StatusCode.Should().Be(200);
            result!.Value.Should().Be(list);
        }

        [Fact]
        public async Task GetAllBooksEndPointShouldReturn204WhenListIsEmpty()
        {
            //Arrange
            var list = new List<GetAllBooksResponse>();

            _sender.Setup(
                x => x.Send(
                    It.IsAny<GetAllBooksQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            //Act
            var result = await _controller.GetAllBooks(default) as StatusCodeResult;

            //Assert
            result!.StatusCode.Should().Be(204);
        }
    }
}
