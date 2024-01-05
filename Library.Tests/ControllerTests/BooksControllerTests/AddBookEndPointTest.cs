using Library.Presentation.Controllers;
using MediatR;
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

            //Act

            //Assert
        }
    }
}
