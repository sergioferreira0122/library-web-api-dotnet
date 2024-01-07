using Library.Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Features.Books.Commands
{
    public class CreateBookCommand : ICommand
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateOnly PublishDate { get; set; }
    }
}