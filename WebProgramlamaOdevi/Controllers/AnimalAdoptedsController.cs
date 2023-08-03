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
            var applicationDbContext = _context.AnimalAdopted.Include(a => a.Animal);
            //Giriş yapmış kullanıcı ile ilgili nesnelerin listelenmesini sağlar.
            return View(await applicationDbContext.Where(p=>p.UserId== _userId).ToListAsync());
        }

        // GET: AnimalAdopteds/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AnimalAdopted == null)
            {
                return NotFound();
            }

            var animalAdopted = await _context.AnimalAdopted
                .Include(a => a.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            //Eğer get yapılan nesne o kullanıcıya ait değilse error verilir.
            if (animalAdopted == null||animalAdopted.UserId!=_userId)
            {
                return NotFound();
            }

            return View(animalAdopted);
        }

        // GET: AnimalAdopteds/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id");
            return View();
        }

        // POST: AnimalAdopteds/Create
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", animalAdopted.AnimalId);
            return View(animalAdopted);
        }

        // GET: AnimalAdopteds/Edit/5
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", animalAdopted.AnimalId);
            return View(animalAdopted);
        }

        // POST: AnimalAdopteds/Edit/5
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
            if (animalAdopted.isConfirmed)
            {
                return NotFound("Onaylanmış İşlemi değiştiremezsiniz.");
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", animalAdopted.AnimalId);
            return View(animalAdopted);
        }

    
        private bool AnimalAdoptedExists(Guid id)
        {
          return (_context.AnimalAdopted?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
