using Library.Application.Abstractions;
using Library.Domain.Abstractions;

namespace Library.Application.Features.Books.Commands
{
    public class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteBookCommand request,
            CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
            if (book == null)
                return BookErrors.BookNotFound;

            await _bookRepository.DeleteBookAsync(book);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}