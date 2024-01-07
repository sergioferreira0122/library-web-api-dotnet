using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ConnectionContext _dbContext;

        public ClientRepository(ConnectionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddClientAsync(
            Client client,
            CancellationToken cancellationToken)
        {
            await _dbContext.Clients.AddAsync(client, cancellationToken);
        }

        public Task DeleteClientAsync(Client client)
        {
            _dbContext.Clients.Remove(client);
            return Task.CompletedTask;
        }

        public async Task<Client?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Clients
                .FirstOrDefaultAsync(client => client.Id == id, cancellationToken);
        }

        public async Task<ICollection<Client>> GetClientsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Clients
                .ToListAsync(cancellationToken);
        }

        public Task UpdateClientAsync(Client client)
        {
            _dbContext.Clients.Entry(client).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}