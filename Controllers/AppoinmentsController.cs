using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedistockApp.Models;

namespace MedistockApp.Controllers
{
    public class AppoinmentsController : Controller
    {
        private readonly MedistockContext _context;

        public AppoinmentsController(MedistockContext context)
        {
            _context = context;
        }

        // GET: Appoinments
        public async Task<IActionResult> Index()
        {
            var medistockContext = _context.Appoinments.Include(a => a.FkIdSchedulingNavigation);
            return View(await medistockContext.ToListAsync());
        }

        // GET: Appoinments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appoinment = await _context.Appoinments
                .Include(a => a.FkIdSchedulingNavigation)
                .FirstOrDefaultAsync(m => m.IdAppointment == id);
            if (appoinment == null)
            {
                return NotFound();
            }

            return View(appoinment);
        }

        // GET: Appoinments/Create
        public IActionResult Create()
        {
            ViewData["FkIdScheduling"] = new SelectList(_context.Schedulings, "IdScheduling", "IdScheduling");
            return View();
        }

        // POST: Appoinments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAppointment,DateHour,FkIdScheduling")] Appoinment appoinment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appoinment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkIdScheduling"] = new SelectList(_context.Schedulings, "IdScheduling", "IdScheduling", appoinment.FkIdScheduling);
            return View(appoinment);
        }

        // GET: Appoinments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appoinment = await _context.Appoinments.FindAsync(id);
            if (appoinment == null)
            {
                return NotFound();
            }
            ViewData["FkIdScheduling"] = new SelectList(_context.Schedulings, "IdScheduling", "IdScheduling", appoinment.FkIdScheduling);
            return View(appoinment);
        }

        // POST: Appoinments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAppointment,DateHour,FkIdScheduling")] Appoinment appoinment)
        {
            if (id != appoinment.IdAppointment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appoinment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppoinmentExists(appoinment.IdAppointment))
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
            ViewData["FkIdScheduling"] = new SelectList(_context.Schedulings, "IdScheduling", "IdScheduling", appoinment.FkIdScheduling);
            return View(appoinment);
        }

        // GET: Appoinments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appoinment = await _context.Appoinments
                .Include(a => a.FkIdSchedulingNavigation)
                .FirstOrDefaultAsync(m => m.IdAppointment == id);
            if (appoinment == null)
            {
                return NotFound();
            }

            return View(appoinment);
        }

        // POST: Appoinments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appoinment = await _context.Appoinments.FindAsync(id);
            if (appoinment != null)
            {
                _context.Appoinments.Remove(appoinment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppoinmentExists(int id)
        {
            return _context.Appoinments.Any(e => e.IdAppointment == id);
        }
    }
}
