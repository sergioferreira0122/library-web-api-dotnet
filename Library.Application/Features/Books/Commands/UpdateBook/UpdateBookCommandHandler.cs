using Library.Application.Abstractions;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Books.Commands
{
    public class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<UpdateBookCommand, Book> _mapper;

        public UpdateBookCommandHandler(
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork,
            IMapper<UpdateBookCommand, Book> mapper)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(
            UpdateBookCommand request,
            CancellationToken cancellationToken)
        {
            var bookFromPersistence = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
            if (bookFromPersistence == null)
                return BookErrors.BookNotFound;

            var book = _mapper.Map(request, bookFromPersistence);

            await _bookRepository.UpdateBookAsync(book);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}
