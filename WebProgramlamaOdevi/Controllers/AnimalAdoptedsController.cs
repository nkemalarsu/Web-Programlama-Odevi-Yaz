using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgramlamaOdevi.Data;
using WebProgramlamaOdevi.Models;

namespace WebProgramlamaOdevi.Controllers
{
    public class AnimalAdoptedsController : Controller
    {
        private readonly ApplicationDbContext _context;
        string _userId = string.Empty;
        public AnimalAdoptedsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            if (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value != string.Empty)
            {
                _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            }
        }

        // GET: AnimalAdopteds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AnimalAdopted.Include(a => a.Animal).Include(u=>u.IdentityUser);
            return View(await applicationDbContext.Where(p => p.UserId == _userId).ToListAsync());
        }

        // GET: AnimalAdopteds/Details/5
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

        // GET: AnimalAdopteds/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal.Where(p=>p.isAdopted==false&&p.isConfirmed==true).ToList(), "Id", "Id");
            return View();
        }

        // POST: AnimalAdopteds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId")] AnimalAdopted animalAdopted)
        {
            try
            {
                animalAdopted.Id = Guid.NewGuid().ToString();
                animalAdopted.UserId= _userId;
                _context.Add(animalAdopted);
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

            }
        }

       

        private bool AnimalAdoptedExists(string id)
        {
          return (_context.AnimalAdopted?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
