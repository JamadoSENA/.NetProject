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
    public class MedicinesprescriptionsController : Controller
    {
        private readonly MedistockContext _context;

        public MedicinesprescriptionsController(MedistockContext context)
        {
            _context = context;
        }

        // GET: Medicinesprescriptions
        public async Task<IActionResult> Index()
        {
            var medistockContext = _context.Medicinesprescriptions.Include(m => m.FkIdMedicineNavigation).Include(m => m.FkIdPrescriptionNavigation);
            return View(await medistockContext.ToListAsync());
        }

        // GET: Medicinesprescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinesprescription = await _context.Medicinesprescriptions
                .Include(m => m.FkIdMedicineNavigation)
                .Include(m => m.FkIdPrescriptionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicinesprescription == null)
            {
                return NotFound();
            }

            return View(medicinesprescription);
        }

        // GET: Medicinesprescriptions/Create
        public IActionResult Create()
        {
            ViewData["FkIdMedicine"] = new SelectList(_context.Medicines, "IdMedicine", "IdMedicine");
            ViewData["FkIdPrescription"] = new SelectList(_context.Prescriptions, "IdPrescription", "IdPrescription");
            return View();
        }

        // POST: Medicinesprescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,FkIdMedicine,FkIdPrescription")] Medicinesprescription medicinesprescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicinesprescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkIdMedicine"] = new SelectList(_context.Medicines, "IdMedicine", "IdMedicine", medicinesprescription.FkIdMedicine);
            ViewData["FkIdPrescription"] = new SelectList(_context.Prescriptions, "IdPrescription", "IdPrescription", medicinesprescription.FkIdPrescription);
            return View(medicinesprescription);
        }

        // GET: Medicinesprescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinesprescription = await _context.Medicinesprescriptions.FindAsync(id);
            if (medicinesprescription == null)
            {
                return NotFound();
            }
            ViewData["FkIdMedicine"] = new SelectList(_context.Medicines, "IdMedicine", "IdMedicine", medicinesprescription.FkIdMedicine);
            ViewData["FkIdPrescription"] = new SelectList(_context.Prescriptions, "IdPrescription", "IdPrescription", medicinesprescription.FkIdPrescription);
            return View(medicinesprescription);
        }

        // POST: Medicinesprescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,FkIdMedicine,FkIdPrescription")] Medicinesprescription medicinesprescription)
        {
            if (id != medicinesprescription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicinesprescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicinesprescriptionExists(medicinesprescription.Id))
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
            ViewData["FkIdMedicine"] = new SelectList(_context.Medicines, "IdMedicine", "IdMedicine", medicinesprescription.FkIdMedicine);
            ViewData["FkIdPrescription"] = new SelectList(_context.Prescriptions, "IdPrescription", "IdPrescription", medicinesprescription.FkIdPrescription);
            return View(medicinesprescription);
        }

        // GET: Medicinesprescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinesprescription = await _context.Medicinesprescriptions
                .Include(m => m.FkIdMedicineNavigation)
                .Include(m => m.FkIdPrescriptionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicinesprescription == null)
            {
                return NotFound();
            }

            return View(medicinesprescription);
        }

        // POST: Medicinesprescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicinesprescription = await _context.Medicinesprescriptions.FindAsync(id);
            if (medicinesprescription != null)
            {
                _context.Medicinesprescriptions.Remove(medicinesprescription);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicinesprescriptionExists(int id)
        {
            return _context.Medicinesprescriptions.Any(e => e.Id == id);
        }
    }
}
