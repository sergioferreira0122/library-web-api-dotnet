using Library.Application.Abstractions;

namespace Library.Application.Features.Books.Commands
{
    public record DeleteBookCommand(int Id) : ICommand {}
}
