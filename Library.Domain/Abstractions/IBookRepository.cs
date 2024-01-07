using Library.Domain.Entities;

namespace Library.Domain.Abstractions
{
    public interface IBookRepository
    {
        Task<ICollection<Book>> GetBooksAsync(CancellationToken cancellationToken);

        Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task AddBookAsync(Book book, CancellationToken cancellationToken);

        Task DeleteBookAsync(Book book);

        Task UpdateBookAsync(Book book);
    }
}