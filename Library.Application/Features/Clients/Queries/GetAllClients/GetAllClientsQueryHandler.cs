using Library.Application.Abstractions;
using Library.Domain.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Clients.Queries
{
    public class GetAllClientsQueryHandler : IQueryHandler<GetAllClientsQuery, ICollection<GetAllClientsResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper<ICollection<Client>, ICollection<GetAllClientsResponse>> _mapper;

        public GetAllClientsQueryHandler(
            IClientRepository clientRepository,
            IMapper<ICollection<Client>, ICollection<GetAllClientsResponse>> mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllClientsResponse>> Handle(
            GetAllClientsQuery request,
            CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetClientsAsync(cancellationToken);

            return _mapper.Map(clients, new List<GetAllClientsResponse>());
        }
    }
}