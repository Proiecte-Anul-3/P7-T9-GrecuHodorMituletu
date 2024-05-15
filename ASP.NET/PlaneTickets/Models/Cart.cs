using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PlaneTickets.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }  // Primary key
        [ForeignKey("userId")]
        public required string UserId { get; set; }  // Foreign key to the Users table
        public decimal Price { get; set; }
        [ForeignKey("TicketId")]
        public int? TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int Quantity { get; set; }

    }
}
