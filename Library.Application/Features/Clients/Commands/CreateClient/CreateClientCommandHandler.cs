using Library.Application.Abstractions;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Clients.Commands
{
    public class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
    {
        private readonly IValidator<CreateClientCommand> _validator;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<CreateClientCommand, Client> _mapper;

        public CreateClientCommandHandler(
            IValidator<CreateClientCommand> validator,
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork,
            IMapper<CreateClientCommand, Client> mapper)
        {
            _validator = validator;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(
            CreateClientCommand request,
            CancellationToken cancellationToken)
        {
            var validatorResult = await _validator.IsValidAsync(request, cancellationToken);
            if (validatorResult.IsFailure)
                return validatorResult;

            var client = _mapper.Map(request, new Client());

            await _clientRepository.AddClientAsync(client, cancellationToken);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}