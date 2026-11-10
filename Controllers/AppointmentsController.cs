using System.Security.Claims;
using AutoSzerviz.Data;
using AutoSzerviz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoSzerviz.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointments (Foglalások listája)
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Lekérjük a felhasználó foglalásait a kapcsolódó autóval és szolgáltatással együtt
            var appointments = await _context.Appointments
                .Include(a => a.Vehicle)
                .Include(a => a.Service)
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return View(appointments);
        }

        // GET: Appointments/Create (Új időpont foglalása)
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userVehicles = await _context.Vehicles.Where(v => v.UserId == userId).ToListAsync();

            if (!userVehicles.Any())
            {
                TempData["ErrorMessage"] = "Előbb rögzítened kell legalább egy járművet a foglaláshoz!";
                return RedirectToAction("Create", "Vehicles");
            }

            ViewData["VehicleId"] = new SelectList(userVehicles, "Id", "LicensePlate");
            ViewData["ServiceId"] = new SelectList(await _context.Services.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Appointments/Create (Mentés)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppointmentDate,VehicleId,ServiceId")] Appointment appointment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                appointment.UserId = userId;
                appointment.Status = "Foglalva";
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var userVehicles = await _context.Vehicles.Where(v => v.UserId == userId).ToListAsync();
            ViewData["VehicleId"] = new SelectList(userVehicles, "Id", "LicensePlate", appointment.VehicleId);
            ViewData["ServiceId"] = new SelectList(await _context.Services.ToListAsync(), "Id", "Name", appointment.ServiceId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointment = await _context.Appointments
                .Include(a => a.Vehicle)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (appointment == null) return NotFound();

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}