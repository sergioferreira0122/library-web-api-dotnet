using Library.Application.Abstractions;
using Library.Application.Features.Clients;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Clients.Commands
{
    public class UpdateClientCommandHandler : ICommandHandler<UpdateClientCommand>
    {
        private readonly IValidator<UpdateClientCommand> _validator;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<UpdateClientCommand, Client> _mapper;

        public UpdateClientCommandHandler(
            IValidator<UpdateClientCommand> validator,
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork,
            IMapper<UpdateClientCommand, Client> mapper)
        {
            _validator = validator;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(
            UpdateClientCommand request,
            CancellationToken cancellationToken)
        {
            var validatorResult = await _validator.IsValidAsync(request, cancellationToken);
            if (validatorResult.IsFailure)
                return validatorResult;

            var clientFromPersistence = await _clientRepository.GetByIdAsync(request.Id, cancellationToken);
            if (clientFromPersistence == null)
                return ClientErrors.ClientNotFound;

            var client = _mapper.Map(request, clientFromPersistence);

            await _clientRepository.UpdateClientAsync(client);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}