namespace Library.Application.Features.Bookings.Queries
{
    public class GetAllBookingsFromClientResponse
    {
        public string? BookTitle { get; set; }
        public string? BookAuthor { get; set; }
        public DateOnly? IssueDate { get; set; }
    }
}
