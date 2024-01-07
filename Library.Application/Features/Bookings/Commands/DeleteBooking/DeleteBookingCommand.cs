using Library.Application.Abstractions;

namespace Library.Application.Features.Bookings.Commands
{
    public record DeleteBookingCommand(int Id) : ICommand { }
}