namespace Library.Application.Features.Bookings
{
    public class BookingErrors
    {
        public static readonly Error BookingNotFound = new(
            "Bookings.NotFound",
            "Booking not found");

        public static readonly Error UpdateBookingNotSameIdFromBodyAndParameter = new(
            "Booking.Update",
            "Booking Id from body is not equals to parameter Booking Id");
    }
}
