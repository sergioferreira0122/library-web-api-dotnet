using Library.Domain.Entities;

namespace Library.Domain.Abstractions
{
    public interface IClientRepository
    {
        Task<ICollection<Client>> GetClientsAsync(CancellationToken cancellationToken);

        Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task AddClientAsync(Client client, CancellationToken cancellationToken);

        Task DeleteClientAsync(Client client);

        Task UpdateClientAsync(Client client);
    }
}