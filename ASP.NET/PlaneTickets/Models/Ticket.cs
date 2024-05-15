using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PlaneTickets.Models
{
    public class Ticket
    {
        [DisplayName("Ticket")]
        [Key]
        public int TicketId { get; set; }
        public required string Departure { get; set; }
        public required string Destination { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string Class { get; set; }
        [DisplayName("Departure Time")]
        public DateTime DepartureTime { get; set; }
        [DisplayName("Destination Time")]
        public DateTime DestinationTime { get; set; }
        [DisplayName("Destion Gate")]
        public required string DestinationGate { get; set; }
        [DisplayName("Departure Gate")]
        public required string DepartureGate { get; set; }
    }
}
