using Library.Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Features.Bookings.Commands
{
    public class UpdateBookingCommand : ICommand
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public DateOnly IssuedDate { get; set; }
    }
}