using Library.Application.Abstractions;
using Library.Domain.Abstractions;

namespace Library.Application.Features.Clients.Commands
{
    public class DeleteClientCommandHandler : ICommandHandler<DeleteClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientCommandHandler(
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteClientCommand request,
            CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.Id, cancellationToken);
            if (client == null)
                return ClientErrors.ClientNotFound;

            await _clientRepository.DeleteClientAsync(client);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}
