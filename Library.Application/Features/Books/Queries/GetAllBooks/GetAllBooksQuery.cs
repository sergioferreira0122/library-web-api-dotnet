using Library.Application.Abstractions;

namespace Library.Application.Features.Books.Queries
{
    public record GetAllBooksQuery() : IQuery<ICollection<GetAllBooksResponse>> {}
}
