namespace Library.Application.Features.Books.Queries
{
    public class GetAllBooksResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public DateOnly? PublishDate { get; set; }
    }
}
