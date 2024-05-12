using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace PlaneTickets.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }  // Primary key

        public int UserId { get; set; }  // Foreign key to the Users table
        public required IdentityUser User { get; set; }  // Might be needed for additional user data

        public decimal TotalPrice { get; set; }

        //ICollection<CartItem> is used for the relationship between ShoppingCart and CartItem
        public required ICollection<CartTicket> CartItems { get; set; }
    }
}
