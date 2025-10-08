using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nexos.Data;
using Nexos.Models;

namespace Nexos.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }
    public IActionResult Definicao()
    {
        return View();
    }

    public IActionResult Mesas()
    {
        ViewData["Title"] = "Mesas Disponíveis";
        return View();
    }

    public IActionResult MesaDetalhes(int id)
    {
        // Busca a mesa pelo id
        CampanhaMesa mesa = 
            _context.CampanhasMesas
            .FirstOrDefault(m => m.ID_Campanha == id);

        if (mesa == null)
        {
            return NotFound();
        }

        return View(mesa);
    }


    public IActionResult Mesas2()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
