using Library.Application.Abstractions;

namespace Library.Application.Features.Clients.Commands
{
    public record DeleteClientCommand(int Id) : ICommand { }
}