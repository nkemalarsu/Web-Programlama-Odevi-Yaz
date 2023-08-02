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

namespace WebProgramlamaOdevi.Controllers
{
    [Authorize]
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            //Sadece kabul edilen ve sahiplendirilmemiş hayvanları görüntüler.
            return _context.Animal != null ?
                        View(await _context.Animal.Where(p=>p.isAdopted==false&&p.isConfirmed==true).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Animal'  is null.");
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Animal == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewBag.AnimalTypes = _context.AnimalType.ToList().Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalTypeId,Name,Description,Age,Id")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                animal.Id = Guid.NewGuid();
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        private bool AnimalExists(Guid id)
        {
            return (_context.Animal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
