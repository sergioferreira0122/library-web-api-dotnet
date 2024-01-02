using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Clients.Commands
{
    public class CreateClientMapper : IMapper<CreateClientCommand, Client>
    {
        public Client Map(
            CreateClientCommand data,
            Client target)
        {
            target.Address = data.Address;
            target.Name = data.Name;
            target.PhoneNumber = data.PhoneNumber;

            return target;
        }
    }
}