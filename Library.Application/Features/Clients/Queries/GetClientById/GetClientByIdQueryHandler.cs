using Library.Application.Abstractions;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Clients.Queries
{
    public class GetClientByIdQueryHandler : IQueryHandler<GetClientByIdQuery, GetClientByIdResponse?>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper<Client, GetClientByIdResponse> _mapper;

        public GetClientByIdQueryHandler(
            IClientRepository clientRepository,
            IMapper<Client, GetClientByIdResponse> mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<GetClientByIdResponse?> Handle(
            GetClientByIdQuery request,
            CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.Id, cancellationToken);

            return client != null ? _mapper.Map(client, new GetClientByIdResponse()) : null;
        }
    }
}