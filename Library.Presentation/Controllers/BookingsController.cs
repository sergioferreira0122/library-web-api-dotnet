using Library.Application;
using Library.Application.Features.Bookings;
using Library.Application.Features.Bookings.Commands;
using Library.Application.Features.Bookings.Queries;
using Library.Application.Features.Books;
using Library.Application.Features.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            var response = await _sender.Send(new GetBookingByIdQuery(bookingId), cancellationToken);

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

            if (resultError.Equals(BookingErrors.BookingNotFound)) return StatusCode(404, resultError);
            if (resultError.Equals(BookErrors.BookNotFound)) return StatusCode(404, resultError);
            if (resultError.Equals(ClientErrors.ClientNotFound)) return StatusCode(404, resultError);

            throw new ArgumentException($"Unexpected error: {resultError}");
        }
    }
}