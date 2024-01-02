using MediatR;

namespace Library.Application.Abstractions
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
