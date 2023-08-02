using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaOdevi.Data;
using WebProgramlamaOdevi.Models;

namespace WebProgramlamaOdevi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnimalAdoptedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalAdoptedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AnimalAdopteds
        public async Task<IActionResult> Index()
        {
              return _context.AnimalAdopted != null ? 
                          View(await _context.AnimalAdopted.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AnimalAdopted'  is null.");
        }

        // GET: Admin/AnimalAdopteds/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AnimalAdopted == null)
            {
                return NotFound();
            }

            var animalAdopted = await _context.AnimalAdopted
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalAdopted == null)
            {
                return NotFound();
            }

            return View(animalAdopted);
        }

        // GET: Admin/AnimalAdopteds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AnimalAdopteds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,AnimalId,CreatedDateTime,isConfirmed,ConfirmedDateTime,Id")] AnimalAdopted animalAdopted)
        {
            if (ModelState.IsValid)
            {
                animalAdopted.Id = Guid.NewGuid();
                _context.Add(animalAdopted);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalAdopted);
        }

        // GET: Admin/AnimalAdopteds/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AnimalAdopted == null)
            {
                return NotFound();
            }

            var animalAdopted = await _context.AnimalAdopted.FindAsync(id);
            if (animalAdopted == null)
            {
                return NotFound();
            }
            return View(animalAdopted);
        }

        // POST: Admin/AnimalAdopteds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,AnimalId,CreatedDateTime,isConfirmed,ConfirmedDateTime,Id")] AnimalAdopted animalAdopted)
        {
            if (id != animalAdopted.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalAdopted);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalAdoptedExists(animalAdopted.Id))
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
            return View(animalAdopted);
        }

        // GET: Admin/AnimalAdopteds/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AnimalAdopted == null)
            {
                return NotFound();
            }

            var animalAdopted = await _context.AnimalAdopted
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalAdopted == null)
            {
                return NotFound();
            }

            return View(animalAdopted);
        }

        // POST: Admin/AnimalAdopteds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AnimalAdopted == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AnimalAdopted'  is null.");
            }
            var animalAdopted = await _context.AnimalAdopted.FindAsync(id);
            if (animalAdopted != null)
            {
                _context.AnimalAdopted.Remove(animalAdopted);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalAdoptedExists(Guid id)
        {
          return (_context.AnimalAdopted?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
