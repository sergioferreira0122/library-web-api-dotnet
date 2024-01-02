using System.ComponentModel.DataAnnotations;
using Library.Application.Abstractions;

namespace Library.Application.Features.Clients.Commands
{
    public class CreateClientCommand : ICommand
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }
    }
}
