﻿using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}