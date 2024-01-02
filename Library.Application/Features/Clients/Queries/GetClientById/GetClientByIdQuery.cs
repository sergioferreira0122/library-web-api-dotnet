using Library.Application.Abstractions;

namespace Library.Application.Features.Clients.Queries
{
    public class GetClientByIdQuery : IQuery<GetClientByIdResponse?>
    {
        public int Id { get; set; }
    }
}