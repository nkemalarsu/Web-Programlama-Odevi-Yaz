using System;
using System.Collections.Generic;
using System.Data;
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
    [Authorize(Roles = "Admin")]
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
            var applicationDbContext = _context.AnimalAdopted.Include(a => a.Animal).Include(u=>u.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AnimalAdopteds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AnimalAdopted == null)
            {
                return NotFound();
            }

            var animalAdopted = await _context.AnimalAdopted
                .Include(a => a.Animal)
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/AnimalAdopteds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,AnimalId,isConfirmed,ConfirmedDateTime")] AnimalAdopted animalAdopted)
        {
            try
            {
                animalAdopted.Id=Guid.NewGuid().ToString();
                _context.Add(animalAdopted);
                if (animalAdopted.isConfirmed)
                {
                   var animal= _context.Animal.FirstOrDefault(p => p.Id == animalAdopted.Id);
                    animal.isAdopted=true;
                    _context.Update(animal);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }

            finally
            {
                ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", animalAdopted.AnimalId);
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", animalAdopted.UserId);
            }
            
        }

        // GET: Admin/AnimalAdopteds/Edit/5
        public async Task<IActionResult> Edit(string id)
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", animalAdopted.AnimalId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id",animalAdopted.Id);
            return View(animalAdopted);
        }

        // POST: Admin/AnimalAdopteds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,AnimalId,CreatedDateTime,isConfirmed,ConfirmedDateTime,Id")] AnimalAdopted animalAdopted)
        {
            if (id != animalAdopted.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (animalAdopted.isConfirmed)
                    {
                        var animal = _context.Animal.FirstOrDefault(p => p.Id == animalAdopted.AnimalId);
                        animal.isAdopted = true;
                        
                    }
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", animalAdopted.AnimalId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", animalAdopted.UserId);
            return View(animalAdopted);
        }

        // GET: Admin/AnimalAdopteds/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AnimalAdopted == null)
            {
                return NotFound();
            }

            var animalAdopted = await _context.AnimalAdopted
                .Include(a => a.Animal)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
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

        private bool AnimalAdoptedExists(string id)
        {
          return (_context.AnimalAdopted?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
