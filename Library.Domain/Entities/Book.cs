using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? Author { get; set; }
        public DateTime? PublishDate { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}