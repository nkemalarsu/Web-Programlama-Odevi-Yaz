using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaOdevi.Data;
using WebProgramlamaOdevi.Models;

namespace WebProgramlamaOdevi.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        string _userId;
        public AnimalsController(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userId=httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Animal.Include(a => a.AnimalType);
            return View(await applicationDbContext.Where(p=>p.isConfirmed==true&&p.isAdopted==false).ToListAsync());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(string id)
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

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Id");
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalTypeId,Name,Description,Age,Id")] Animal animal)
        {
            try
            {
                animal.Id = Guid.NewGuid().ToString();
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
            finally
            {
                ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Id", animal.AnimalTypeId);
            }
        }

        // GET: Animals/Edit/5
       
        private bool AnimalExists(string id)
        {
          return (_context.Animal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
