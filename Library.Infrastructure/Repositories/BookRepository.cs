using Library.Domain.Abstractions;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ConnectionContext _dbContext;

        public BookRepository(ConnectionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBookAsync(
            Book book,
            CancellationToken cancellationToken)
        {
            await _dbContext.Books.AddAsync(book, cancellationToken);
        }

        public Task DeleteBookAsync(Book book)
        {
            _dbContext.Books.Remove(book);
            return Task.CompletedTask;
        }

        public async Task<ICollection<Book>> GetBooksAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Books
                .ToListAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Books
                .FirstOrDefaultAsync(book => book.Id == id, cancellationToken);
        }

        public Task UpdateBookAsync(Book book)
        {
            _dbContext.Books.Entry(book).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
