using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Clients.Queries
{
    public class GetAllClientsMapper : IMapper<ICollection<Client>, ICollection<GetAllClientsResponse>>
    {
        public ICollection<GetAllClientsResponse> Map(
            ICollection<Client> data,
            ICollection<GetAllClientsResponse> target)
        {
            if (data.Count == 0)
                return target;

            foreach (var client in data)
            {
                target.Add(IndividualMapping(client));
            }

            return target;
        }

        private static GetAllClientsResponse IndividualMapping(Client data)
        {
            var readModel = new GetAllClientsResponse
            {
                Id = data.Id,
                Name = data.Name,
                Address = data.Address,
                PhoneNumber = data.PhoneNumber
            };

            return readModel;
        }
    }
}