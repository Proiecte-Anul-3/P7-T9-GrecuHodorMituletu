using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlaneTickets.Data;
using PlaneTickets.Models;

namespace PlaneTickets.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CartsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Carts
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var applicationDbContext = _context.Cart.Include(c => c.Ticket)
                                                        .Where(c => c.UserId == userId); // Filter by user ID
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Ticket)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        [Authorize (Roles ="Admin")]
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Ticket, "TicketId", "TicketId");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("CartId,UserId,Price,TicketId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Ticket, "TicketId", "TicketId", cart.TicketId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Ticket, "TicketId", "TicketId", cart.TicketId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,UserId,Price,TicketId")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
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
            ViewData["TicketId"] = new SelectList(_context.Ticket, "TicketId", "TicketId", cart.TicketId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Ticket)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cart == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cart'  is null.");
            }
            var cart = await _context.Cart.FindAsync(id);
            if (cart != null)
            {
                _context.Cart.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return (_context.Cart?.Any(e => e.CartId == id)).GetValueOrDefault();
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            Ticket ticketAddToCart = await _context.Ticket.FirstOrDefaultAsync(u => u.TicketId == id);
            var checkifUserSignedInOrNot = _signInManager.IsSignedIn(User);
            if (checkifUserSignedInOrNot)
            {


                var user = _userManager.GetUserId(User);
                if (user != null)
                {
                    //Check if the signed user has any cart or not?
                    var getTheCartIfAnyExistForTheUser = await _context.Cart.Where(u => u.UserId.Contains(user)).ToListAsync();
                    if (getTheCartIfAnyExistForTheUser.Count() > 0)
                    {
                        //check if the item is already in the cart or not
                        var getTheQuantity = getTheCartIfAnyExistForTheUser.FirstOrDefault(p => p.TicketId == id);
                        if (getTheQuantity != null)
                        { //if the item is already in the cart just increase the quantity by 1 and update the cart.
                            getTheQuantity.Quantity = getTheQuantity.Quantity + 1;
                            _context.Cart.Update(getTheQuantity);
                            getTheQuantity.Price += getTheQuantity.Price;
                        }
                        else
                        { // User has a cart but addding a new item to the existing cart.
                            Cart newItemToCart = new Cart
                            {
                                TicketId = id,
                                Price = ticketAddToCart.Price,
                                Ticket = ticketAddToCart,
                                UserId = user,
                                Quantity = 1,
                            };
                            await _context.Cart.AddAsync(newItemToCart);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        // User has no cart. Addding a brand new cart for the user.
                        Cart newItemToCart = new Cart
                        {
                            TicketId = id,
                            Price = ticketAddToCart.Price,
                            Ticket = ticketAddToCart,
                            UserId = user,
                            Quantity = 1,
                        };
                        await _context.Cart.AddAsync(newItemToCart);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
