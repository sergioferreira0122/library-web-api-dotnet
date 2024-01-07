using Library.Application.Abstractions;
using Library.Application.Features.Books.Queries;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Queries
{
    public class GetAllBooksQueryHandler : IQueryHandler<GetAllBooksQuery, ICollection<GetAllBooksResponse>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper<ICollection<Book>, ICollection<GetAllBooksResponse>> _mapper;

        public GetAllBooksQueryHandler(
            IBookRepository bookRepository,
            IMapper<ICollection<Book>, ICollection<GetAllBooksResponse>> mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllBooksResponse>> Handle(
            GetAllBooksQuery request,
            CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetBooksAsync(cancellationToken);

            return _mapper.Map(books, new List<GetAllBooksResponse>());
        }
    }
}