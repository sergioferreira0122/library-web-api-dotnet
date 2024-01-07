using Library.Application.Abstractions;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Books.Queries
{
    public class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, GetBookByIdResponse?>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper<Book, GetBookByIdResponse> _mapper;

        public GetBookByIdQueryHandler(
            IBookRepository bookRepository,
            IMapper<Book, GetBookByIdResponse> mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<GetBookByIdResponse?> Handle(
            GetBookByIdQuery request,
            CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);

            return book != null ? _mapper.Map(book, new GetBookByIdResponse()) : null;
        }
    }
}