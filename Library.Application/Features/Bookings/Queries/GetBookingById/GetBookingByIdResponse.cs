namespace Library.Application.Features.Bookings.Queries
{
    public class GetBookingByIdResponse
    {
        public int Id { get; set; }
        public string? ClientName { get; set; }
        public string? BookTitle { get; set; }
        public DateOnly? IssuedDate { get; set; }
    }
}
