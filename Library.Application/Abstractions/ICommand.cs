using MediatR;

namespace Library.Application.Abstractions
{
    public interface ICommand : IRequest<Result>
    {
    }
}
