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
    public class AnimalTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AnimalTypes
        public async Task<IActionResult> Index()
        {
              return _context.AnimalType != null ? 
                          View(await _context.AnimalType.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AnimalType'  is null.");
        }

        // GET: Admin/AnimalTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AnimalType == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalType == null)
            {
                return NotFound();
            }

            return View(animalType);
        }

        // GET: Admin/AnimalTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AnimalTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] AnimalType animalType)
        {
            if (ModelState.IsValid)
            {
                animalType.Id = Guid.NewGuid();
                _context.Add(animalType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalType);
        }

        // GET: Admin/AnimalTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AnimalType == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalType.FindAsync(id);
            if (animalType == null)
            {
                return NotFound();
            }
            return View(animalType);
        }

        // POST: Admin/AnimalTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] AnimalType animalType)
        {
            if (id != animalType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalTypeExists(animalType.Id))
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
            return View(animalType);
        }

        // GET: Admin/AnimalTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AnimalType == null)
            {
                return NotFound();
            }

            var animalType = await _context.AnimalType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalType == null)
            {
                return NotFound();
            }

            return View(animalType);
        }

        // POST: Admin/AnimalTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AnimalType == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AnimalType'  is null.");
            }
            var animalType = await _context.AnimalType.FindAsync(id);
            if (animalType != null)
            {
                _context.AnimalType.Remove(animalType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalTypeExists(Guid id)
        {
          return (_context.AnimalType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
