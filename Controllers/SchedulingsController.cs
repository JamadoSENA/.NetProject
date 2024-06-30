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
    public class SchedulingsController : Controller
    {
        private readonly MedistockContext _context;

        public SchedulingsController(MedistockContext context)
        {
            _context = context;
        }

        // GET: Schedulings
        public async Task<IActionResult> Index()
        {
            var medistockContext = _context.Schedulings.Include(s => s.FkIdDoctorNavigation).Include(s => s.FkIdPatientNavigation);
            return View(await medistockContext.ToListAsync());
        }

        // GET: Schedulings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduling = await _context.Schedulings
                .Include(s => s.FkIdDoctorNavigation)
                .Include(s => s.FkIdPatientNavigation)
                .FirstOrDefaultAsync(m => m.IdScheduling == id);
            if (scheduling == null)
            {
                return NotFound();
            }

            return View(scheduling);
        }

        // GET: Schedulings/Create
        public IActionResult Create()
        {
            ViewData["FkIdDoctor"] = new SelectList(_context.Users, "IdUser", "IdUser");
            ViewData["FkIdPatient"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return View();
        }

        // POST: Schedulings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdScheduling,Reason,State,FkIdPatient,FkIdDoctor")] Scheduling scheduling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkIdDoctor"] = new SelectList(_context.Users, "IdUser", "IdUser", scheduling.FkIdDoctor);
            ViewData["FkIdPatient"] = new SelectList(_context.Users, "IdUser", "IdUser", scheduling.FkIdPatient);
            return View(scheduling);
        }

        // GET: Schedulings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduling = await _context.Schedulings.FindAsync(id);
            if (scheduling == null)
            {
                return NotFound();
            }
            ViewData["FkIdDoctor"] = new SelectList(_context.Users, "IdUser", "IdUser", scheduling.FkIdDoctor);
            ViewData["FkIdPatient"] = new SelectList(_context.Users, "IdUser", "IdUser", scheduling.FkIdPatient);
            return View(scheduling);
        }

        // POST: Schedulings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdScheduling,Reason,State,FkIdPatient,FkIdDoctor")] Scheduling scheduling)
        {
            if (id != scheduling.IdScheduling)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchedulingExists(scheduling.IdScheduling))
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
            ViewData["FkIdDoctor"] = new SelectList(_context.Users, "IdUser", "IdUser", scheduling.FkIdDoctor);
            ViewData["FkIdPatient"] = new SelectList(_context.Users, "IdUser", "IdUser", scheduling.FkIdPatient);
            return View(scheduling);
        }

        // GET: Schedulings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduling = await _context.Schedulings
                .Include(s => s.FkIdDoctorNavigation)
                .Include(s => s.FkIdPatientNavigation)
                .FirstOrDefaultAsync(m => m.IdScheduling == id);
            if (scheduling == null)
            {
                return NotFound();
            }

            return View(scheduling);
        }

        // POST: Schedulings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduling = await _context.Schedulings.FindAsync(id);
            if (scheduling != null)
            {
                _context.Schedulings.Remove(scheduling);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchedulingExists(int id)
        {
            return _context.Schedulings.Any(e => e.IdScheduling == id);
        }
    }
}
