using Library.Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Features.Books.Commands
{
    public class UpdateBookCommand : ICommand
    {
        [Required]
        public required int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Author { get; set; }

        [Required]
        public required DateOnly PublishDate { get; set; }
    }
}
