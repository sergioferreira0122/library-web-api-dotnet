using Library.Application.Abstractions;
using Library.Domain.Abstractions;

namespace Library.Application.Features.Bookings.Commands
{
    public class DeleteBookingCommandHandler : ICommandHandler<DeleteBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookingCommandHandler(
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteBookingCommand request,
            CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.Id, cancellationToken);
            if (booking == null)
                return BookingErrors.BookingNotFound;
            
            await _bookingRepository.DeleteBookingAsync(booking);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}
