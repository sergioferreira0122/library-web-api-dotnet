using Library.Application.Abstractions;
using Library.Application.Features.Books;
using Library.Application.Features.Clients;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Bookings.Commands
{
    public class CreateBookingCommandHandler : ICommandHandler<CreateBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookingCommandHandler(
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork,
            IClientRepository clientRepository,
            IBookRepository bookRepository)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _clientRepository = clientRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Result> Handle(
            CreateBookingCommand request,
            CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);
            if (client == null)
                return ClientErrors.ClientNotFound;

            var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);
            if (book == null)
                return BookErrors.BookNotFound;

            var booking = new Booking
            {
                Book = book,
                Client = client,
                IssueDate = request.IssuedDate.ToDateTime(new TimeOnly()),
            };

            await _bookingRepository.AddBookingAsync(booking, cancellationToken);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}