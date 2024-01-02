using System.ComponentModel.DataAnnotations;
using Library.Application.Abstractions;

namespace Library.Application.Features.Clients.Commands
{
    public class UpdateClientCommand : ICommand
    {
        [Required]
        public required int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }
    }
}
