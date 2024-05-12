using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PlaneTickets.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public required string Departure { get; set; }
        public required string Destination { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string Class { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime DestinationTime { get; set; }
        public required string DestinationGate { get; set; }
        public required string DepartureGate { get; set; }
    }
}
