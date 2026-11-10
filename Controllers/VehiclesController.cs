using System.Security.Claims;
using AutoSzerviz.Data;
using AutoSzerviz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoSzerviz.Controllers
{
    [Authorize] // Csak bejelentkezett felhasználók érhetik el!
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles (Saját autók listája)
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var vehicles = await _context.Vehicles
                .Where(v => v.UserId == userId)
                .ToListAsync();
            return View(vehicles);
        }

        // GET: Vehicles/Create (Új autó űrlap)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create (Mentés)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LicensePlate,MakeAndModel,Year")] Vehicle vehicle)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                vehicle.UserId = userId;
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (vehicle == null) return NotFound();

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}