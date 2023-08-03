using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebProgramlamaOdevi.Data;
using WebProgramlamaOdevi.Models;

namespace WebProgramlamaOdevi.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext _context)
        {
            _logger = logger;
            _userManager = userManager;
            this._context = _context;
          
        }

        public async Task<IActionResult> Index()
        {

            //Tek seferlik rol tanımlama işlemi
           /* var roleStore = new RoleStore<IdentityRole>(_context); //Pass the instance of your DbContext here
            var roleManager = new RoleManager<IdentityRole>(roleStore,null,null,null,null);
          await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            await _userManager.AddToRoleAsync(await _userManager.FindByIdAsync(_userId), "Admin");*/
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}