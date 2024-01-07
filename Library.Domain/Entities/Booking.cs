using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public Client? Client { get; set; }
        public Book? Book { get; set; }
        public DateTime? IssueDate { get; set; }
    }
}