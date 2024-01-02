using Library.Application.Abstractions;
using Library.Domain.Entities;

namespace Library.Application.Features.Clients.Commands
{
    public class UpdateClientMapper : IMapper<UpdateClientCommand, Client>
    {
        public Client Map(
            UpdateClientCommand data,
            Client target)
        {
            target.PhoneNumber = data.PhoneNumber;
            target.Address = data.Address;
            target.Name = data.Name;

            return target;
        }
    }
}