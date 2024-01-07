namespace Library.Application.Features.Bookings.Queries
{
    public class GetAllBookingsResponse
    {
        public int Id { get; set; }
        public string? ClientName { get; set; }
        public string? BookTitle { get; set; }
        public DateOnly? IssuedDate { get; set; }
    }
}