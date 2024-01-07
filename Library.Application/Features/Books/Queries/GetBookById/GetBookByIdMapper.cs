using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Books.Queries
{
    public class GetBookByIdMapper : IMapper<Book, GetBookByIdResponse>
    {
        public GetBookByIdResponse Map(
            Book data,
            GetBookByIdResponse target)
        {
            target.Id = data.Id;
            target.Author = data.Author;
            target.PublishDate = DateOnly.FromDateTime(data.PublishDate!.Value);
            target.Title = data.Title;

            return target;
        }
    }
}