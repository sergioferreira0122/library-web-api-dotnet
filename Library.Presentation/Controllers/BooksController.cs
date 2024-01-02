using Library.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Library.Application.Features.Books.Queries;
using Library.Application.Features.Books;
using Library.Application.Features.Books.Commands;

namespace Library.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ISender _sender;

        public BooksController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookById(
            int bookId,
            CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new GetBookByIdQuery() { Id = bookId }, cancellationToken);

            if (response == null) return StatusCode(404, BookErrors.BookNotFound);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new GetAllBooksQuery(), cancellationToken);

            if (response.Count == 0)
                return StatusCode(204);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(
            [FromBody] CreateBookCommand createBookCommand,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(createBookCommand, cancellationToken);

            if (result.IsSuccess) return StatusCode(201);

            return HandleBookErrors(result);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(
            int bookId,
            [FromBody] UpdateBookCommand updateBookCommand,
            CancellationToken cancellationToken)
        {
            if (bookId != updateBookCommand.Id)
                return StatusCode(400, BookErrors.UpdateBookNotSameIdFromBodyAndParameter);

            var result = await _sender.Send(updateBookCommand, cancellationToken);

            if (result.IsSuccess) return StatusCode(201);

            return HandleBookErrors(result);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(
            int bookId,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new DeleteBookCommand(bookId), cancellationToken);

            if (result.IsSuccess) return StatusCode(204);

            return HandleBookErrors(result);
        }

        private IActionResult HandleBookErrors(Result result)
        {
            var resultError = result.Error;

            if (resultError.Equals(BookErrors.BookNotFound)) return StatusCode(404, result);

            throw new Exception($"Unexpected error: {resultError}");
        }
    }
}
