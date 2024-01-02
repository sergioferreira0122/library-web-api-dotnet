using Library.Application.Abstractions;

namespace Library.Application.Features.Clients.Queries
{
    public record GetAllClientsQuery : IQuery<ICollection<GetAllClientsResponse>> { }
}
