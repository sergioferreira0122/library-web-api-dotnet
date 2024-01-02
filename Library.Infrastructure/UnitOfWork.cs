using Library.Domain.Abstractions;

namespace Library.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConnectionContext _connectionContext;

        public UnitOfWork(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public async Task Commit(CancellationToken cancellationToken)
        {
            await _connectionContext.SaveChangesAsync(cancellationToken);
        }
    }
}