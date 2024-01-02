using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Books.Commands
{
    public class UpdateBookMapper : IMapper<UpdateBookCommand, Book>
    {
        public Book Map(
            UpdateBookCommand data,
            Book target)
        {
            target.Id = data.Id;
            target.Author = data.Author;
            target.Title = data.Title;
            target.PublishDate = data.PublishDate.ToDateTime(new TimeOnly());

            return target;
        }
    }
}