using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Clients.Queries
{
    public class GetClientByIdMapper : IMapper<Client, GetClientByIdResponse>
    {
        public GetClientByIdResponse Map(
            Client data,
            GetClientByIdResponse target)
        {
            target.Id = data.Id;
            target.PhoneNumber = data.PhoneNumber;
            target.Address = data.Address;
            target.Name = data.Name;

            return target;
        }
    }
}
