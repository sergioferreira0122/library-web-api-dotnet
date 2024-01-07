using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Books.Queries
{
    public class GetAllBooksMapper : IMapper<ICollection<Book>, ICollection<GetAllBooksResponse>>
    {
        public ICollection<GetAllBooksResponse> Map(
            ICollection<Book> data,
            ICollection<GetAllBooksResponse> target)
        {
            if (data.Count == 0)
                return target;

            foreach (var book in data)
            {
                target.Add(IndividualMapping(book));
            }

            return target;
        }

        private static GetAllBooksResponse IndividualMapping(Book data)
        {
            var readModel = new GetAllBooksResponse
            {
                Id = data.Id,
                Author = data.Author,
                PublishDate = DateOnly.FromDateTime(data.PublishDate!.Value),
                Title = data.Title,
            };

            return readModel;
        }
    }
}