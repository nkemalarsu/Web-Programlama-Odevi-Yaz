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
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Animals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Animal.Include(a => a.AnimalType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Animals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.AnimalType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Admin/Animals/Create
        public IActionResult Create()
        {
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Id");
            return View();
        }

        // POST: Admin/Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalAcceptedId,AnimalTypeId,Name,Description,Age,isAdopted,isConfirmed,Id")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                animal.Id = Guid.NewGuid();
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Id", animal.AnimalTypeId);
            return View(animal);
        }

        // GET: Admin/Animals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Id", animal.AnimalTypeId);
            return View(animal);
        }

        // POST: Admin/Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AnimalAcceptedId,AnimalTypeId,Name,Description,Age,isAdopted,isConfirmed,Id")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
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
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Id", animal.AnimalTypeId);
            return View(animal);
        }

        // GET: Admin/Animals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.AnimalType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Admin/Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Animal == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Animal'  is null.");
            }
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {
                _context.Animal.Remove(animal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(Guid id)
        {
          return (_context.Animal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
