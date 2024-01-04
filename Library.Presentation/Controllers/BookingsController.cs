using Library.Application.Features.Clients.Commands;
using Library.Application.Features.Clients.Queries;
using Library.Application.Features.Clients;
using Library.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Library.Application.Features.Books.Queries;
using Library.Application.Features.Bookings;
using Library.Application.Features.Bookings.Queries;
using Library.Application.Features.Bookings.Commands;

namespace Library.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ISender _sender;

        public BookingsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetBookingById(
            int bookingId,
            CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new GetBookByIdQuery() { Id = bookingId }, cancellationToken);

            if (response == null) return StatusCode(404, BookingErrors.BookingNotFound);

            return Ok(response);
        }

        [HttpGet("{clientId}/Livros")]
        public async Task<IActionResult> GetAllBookingsFromClient(
            int clientId,
            CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new GetAllBookingsFromClientQuery(clientId), cancellationToken);

            if (response == null)
                return StatusCode(404, ClientErrors.ClientNotFound);

            if (response.Count == 0)
                return StatusCode(204);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings(CancellationToken cancellationToken)
        {
            var response = await _sender.Send(new GetAllBookingsQuery(), cancellationToken);

            if (response.Count == 0)
                return StatusCode(204);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(
            [FromBody] CreateBookingCommand createBookingCommand,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(createBookingCommand, cancellationToken);

            if (result.IsSuccess) return StatusCode(201);

            return HandleBookingErrors(result);
        }

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(
            int bookingId,
            [FromBody] UpdateBookingCommand updateBookingCommand,
            CancellationToken cancellationToken)
        {
            if (bookingId != updateBookingCommand.BookingId)
                return StatusCode(400, BookingErrors.UpdateBookingNotSameIdFromBodyAndParameter);

            var result = await _sender.Send(updateBookingCommand, cancellationToken);

            if (result.IsSuccess) return StatusCode(201);

            return HandleBookingErrors(result);
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(
            int bookingId,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new DeleteBookingCommand(bookingId), cancellationToken);

            if (result.IsSuccess) return StatusCode(204);

            return HandleBookingErrors(result);
        }

        private IActionResult HandleBookingErrors(Result result)
        {
            var resultError = result.Error;

            if (resultError.Equals(BookingErrors.BookingNotFound)) return StatusCode(404, result);

            throw new Exception($"Unexpected error: {resultError}");
        }
    }
}
