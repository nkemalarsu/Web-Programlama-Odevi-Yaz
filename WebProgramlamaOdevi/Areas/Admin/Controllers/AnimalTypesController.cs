using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaOdevi.Data;
using WebProgramlamaOdevi.Models;

namespace WebProgramlamaOdevi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
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
        public async Task<IActionResult> Details(string id)
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
        public async Task<IActionResult> Create([Bind("Name")] AnimalType animalType)
        {
            try
            {
                animalType.Id = Guid.NewGuid().ToString();

                _context.Add(animalType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
          
            
           
        }

        // GET: Admin/AnimalTypes/Edit/5
        public async Task<IActionResult> Edit(string id)
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
        public async Task<IActionResult> Edit(string id, [Bind("Name,Id")] AnimalType animalType)
        {
            if (id != animalType.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(animalType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!AnimalTypeExists(animalType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                    return View(ex.Message); 
                    }
                }
                return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Admin/AnimalTypes/Delete/5
        public async Task<IActionResult> Delete(string id)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
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

        private bool AnimalTypeExists(string id)
        {
          return (_context.AnimalType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
