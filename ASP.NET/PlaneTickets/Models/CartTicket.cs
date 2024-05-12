using System.ComponentModel.DataAnnotations;

namespace PlaneTickets.Models
{
    public class CartTicket
    {
        [Key]
        public int CartTicketId { get; set; }  // Primary key

        public int CartId { get; set; }  // Foreign key to the ShoppingCart table
        public required Cart ShoppingCart { get; set; }  // Might be needed for additional shopping cart data

        public int TicketId { get; set; }  // Foreign key to the Products table
        public required Ticket Ticket { get; set; }  // Might be needed for additional product data

        public int Quantity { get; set; }
    }
}
