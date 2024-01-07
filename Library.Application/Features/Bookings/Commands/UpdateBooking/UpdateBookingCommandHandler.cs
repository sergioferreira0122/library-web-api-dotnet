using Library.Application.Abstractions;
using Library.Application.Features.Books;
using Library.Application.Features.Clients;
using Library.Domain.Abstractions;

namespace Library.Application.Features.Bookings.Commands
{
    public class UpdateBookingCommandHandler : ICommandHandler<UpdateBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookingCommandHandler(
            IBookingRepository bookingRepository,
            IClientRepository clientRepository,
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _clientRepository = clientRepository;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateBookingCommand request,
            CancellationToken cancellationToken)
        {
            var bookingFromPersintence = await _bookingRepository.GetByIdAsync(request.BookId, cancellationToken);
            if (bookingFromPersintence == null)
                return BookingErrors.BookingNotFound;

            var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);
            if (client == null)
                return ClientErrors.ClientNotFound;

            var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);
            if (book == null)
                return BookErrors.BookNotFound;

            bookingFromPersintence.IssueDate = request.IssuedDate.ToDateTime(new TimeOnly());
            bookingFromPersintence.Client = client;
            bookingFromPersintence.Book = book;

            await _bookingRepository.UpdateBookingAsync(bookingFromPersintence);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}