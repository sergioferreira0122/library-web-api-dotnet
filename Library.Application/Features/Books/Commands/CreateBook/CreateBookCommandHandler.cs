using Library.Application.Abstractions;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Books.Commands
{
    public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<CreateBookCommand, Book> _mapper;

        public CreateBookCommandHandler(
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork,
            IMapper<CreateBookCommand, Book> mapper)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(
            CreateBookCommand request,
            CancellationToken cancellationToken)
        {
            var book = _mapper.Map(request, new Book());

            await _bookRepository.AddBookAsync(book, cancellationToken);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}
