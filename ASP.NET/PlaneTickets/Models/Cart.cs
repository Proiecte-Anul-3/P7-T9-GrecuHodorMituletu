using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaneTickets.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }  // Primary key

        public int UserId { get; set; }  // Foreign key to the Users table
        public User User { get; set; }  // Might be needed for additional user data

        public decimal TotalPrice { get; set; }

        // ICollection<CartItem> is used for the relationship between ShoppingCart and CartItem
        public ICollection<CartTicket> CartItems { get; set; }
    }
}
