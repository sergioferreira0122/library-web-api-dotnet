using Library.Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Features.Bookings.Commands
{
    public class CreateBookingCommand : ICommand
    {
        [Required]
        public required int ClientId { get; set; }

        [Required]
        public required int BookId { get; set; }

        [Required]
        public required DateOnly IssuedDate { get; set; }
    }
}