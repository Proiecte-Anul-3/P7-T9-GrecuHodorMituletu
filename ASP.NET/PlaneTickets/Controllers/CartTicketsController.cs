using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlaneTickets.Data;
using PlaneTickets.Models;

namespace PlaneTickets.Controllers
{
    public class CartTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CartTickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CartTicket.Include(c => c.ShoppingCart).Include(c => c.Ticket);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CartTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CartTicket == null)
            {
                return NotFound();
            }

            var cartTicket = await _context.CartTicket
                .Include(c => c.ShoppingCart)
                .Include(c => c.Ticket)
                .FirstOrDefaultAsync(m => m.CartTicketId == id);
            if (cartTicket == null)
            {
                return NotFound();
            }

            return View(cartTicket);
        }

        // GET: CartTickets/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Set<Cart>(), "CartId", "CartId");
            ViewData["TicketId"] = new SelectList(_context.Ticket, "TicketId", "TicketId");
            return View();
        }

        // POST: CartTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartTicketId,CartId,TicketId,Quantity")] CartTicket cartTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Set<Cart>(), "CartId", "CartId", cartTicket.CartId);
            ViewData["TicketId"] = new SelectList(_context.Ticket, "TicketId", "TicketId", cartTicket.TicketId);
            return View(cartTicket);
        }

        // GET: CartTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartTicket == null)
            {
                return NotFound();
            }

            var cartTicket = await _context.CartTicket.FindAsync(id);
            if (cartTicket == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Set<Cart>(), "CartId", "CartId", cartTicket.CartId);
            ViewData["TicketId"] = new SelectList(_context.Ticket, "TicketId", "TicketId", cartTicket.TicketId);
            return View(cartTicket);
        }

        // POST: CartTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartTicketId,CartId,TicketId,Quantity")] CartTicket cartTicket)
        {
            if (id != cartTicket.CartTicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartTicketExists(cartTicket.CartTicketId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Set<Cart>(), "CartId", "CartId", cartTicket.CartId);
            ViewData["TicketId"] = new SelectList(_context.Ticket, "TicketId", "TicketId", cartTicket.TicketId);
            return View(cartTicket);
        }

        // GET: CartTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CartTicket == null)
            {
                return NotFound();
            }

            var cartTicket = await _context.CartTicket
                .Include(c => c.ShoppingCart)
                .Include(c => c.Ticket)
                .FirstOrDefaultAsync(m => m.CartTicketId == id);
            if (cartTicket == null)
            {
                return NotFound();
            }

            return View(cartTicket);
        }

        // POST: CartTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartTicket == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CartTicket'  is null.");
            }
            var cartTicket = await _context.CartTicket.FindAsync(id);
            if (cartTicket != null)
            {
                _context.CartTicket.Remove(cartTicket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartTicketExists(int id)
        {
          return (_context.CartTicket?.Any(e => e.CartTicketId == id)).GetValueOrDefault();
        }
    }
}
