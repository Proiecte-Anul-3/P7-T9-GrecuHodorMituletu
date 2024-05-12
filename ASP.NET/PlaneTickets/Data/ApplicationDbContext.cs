using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlaneTickets.Models;

namespace PlaneTickets.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PlaneTickets.Models.Ticket> Ticket { get; set; } = default!;
        public DbSet<PlaneTickets.Models.CartTicket> CartTicket { get; set; } = default!;
        public DbSet<PlaneTickets.Models.Cart> Cart { get; set; } = default!;
    }
}
