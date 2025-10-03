using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nexos.Data;
using Nexos.Models;
using System.Linq;

namespace Nexos.Controllers
{
    public class MesasController : Controller
    {
        private readonly ILogger<MesasController> _logger;
        private readonly AppDbContext _context;

        // Apenas um construtor com todas as dependências
        public MesasController(AppDbContext context, ILogger<MesasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /Mesas
        public IActionResult Index()
        {
            ViewData["Title"] = "Mesas Disponíveis";

            return View("Mesas"); // ou Index, dependendo do nome da sua View
        }

        // Exemplo de detalhes de uma mesa
        public IActionResult MesaDetalhes(int id)
        {
            // Busca a mesa pelo id
            CampanhaMesa mesa = _context.CampanhasMesas
                                        .FirstOrDefault(m => m.ID_Campanha == id);

            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }
    }
}