using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexos.Data;
using System.Linq;
using System.Threading.Tasks;
using Nexos.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Nexos.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(AppDbContext context, ILogger<AdminController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminDashboardVM
            {
                GeneroCount = await _context.Generos.CountAsync(),
                SistemaCount = await _context.Sistemas.CountAsync(),
                MesaCount = await _context.CampanhasMesas.CountAsync() // Assumindo que CampanhasMesas é o DbSet para Mesas
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}

