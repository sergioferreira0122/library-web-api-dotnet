using Library.Application.Abstractions;

namespace Library.Application.Features.Books.Queries
{
    public class GetBookByIdQuery : IQuery<GetBookByIdResponse?>
    {
        public int Id { get; set; }
    }
}
