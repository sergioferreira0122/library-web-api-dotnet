using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Books.Commands
{
    public class CreateBookMapper : IMapper<CreateBookCommand, Book>
    {
        public Book Map(
            CreateBookCommand data,
            Book target)
        {
            target.Author = data.Author;
            target.Title = data.Title;
            target.PublishDate = data.PublishDate.ToDateTime(new TimeOnly());

            return target;
        }
    }
}