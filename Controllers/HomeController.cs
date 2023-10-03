using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using aluguel.Models;
using aluguel.ViewModel;
using aluguel.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace aluguel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext Context)
        {
            _context = Context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var home = await _context.Anuncios
                                .Include(a => a.item)
                                .Where(a => a.iduser != HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                                .ToListAsync();
                return View(home);
            }
            else
            {
                var home = await _context.Anuncios
                                .Include(a => a.item)
                                .ToListAsync();
                return View(home);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Anuncios == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios
                .Include(a => a.item)
                .FirstOrDefaultAsync(m => m.id == id);
            if (anuncio == null)
            {
                return NotFound();
            }

            return View(anuncio);
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