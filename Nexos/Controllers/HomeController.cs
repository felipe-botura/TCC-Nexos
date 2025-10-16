using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // Buscar as 3 últimas campanhas criadas
        var ultimasCampanhas = _context.CampanhasMesas
            .Include(c => c.Genero)
            .Include(c => c.Sistema)
            .Include(c => c.Mestre)
            .OrderByDescending(c => c.DataCriacao)
            .Take(3)
            .ToList();
        
        // Buscar 2 campanhas para a seção "Explore mais"
        var campanhasExplore = _context.CampanhasMesas
            .Include(c => c.Genero)
            .Include(c => c.Sistema)
            .Include(c => c.Mestre)
            .OrderByDescending(c => c.DataCriacao)
            .Skip(3)
            .Take(2)
            .ToList();
        
        ViewBag.CampanhasExplore = campanhasExplore;
        
        return View(ultimasCampanhas);
    }

    public IActionResult AboutUs()
    {
        return View();
    }
    
    public IActionResult Definicao()
    {
        return View();
    }

    public IActionResult Mesas(int? generoId, int? sistemaId, string modalidade, string categoria)
    {
        ViewData["Title"] = "Mesas Disponíveis";
        
        // Carregar dados para os filtros
        ViewBag.Generos = _context.Generos.ToList();
        ViewBag.Sistemas = _context.Sistemas.ToList();
        
        // Query base
        var query = _context.CampanhasMesas
            .Include(c => c.Genero)
            .Include(c => c.Sistema)
            .Include(c => c.Mestre)
            .AsQueryable();

        // Aplicar filtros
        if (generoId.HasValue && generoId.Value > 0)
        {
            query = query.Where(m => m.ID_Genero == generoId.Value);
        }

        if (sistemaId.HasValue && sistemaId.Value > 0)
        {
            query = query.Where(m => m.ID_Sistema == sistemaId.Value);
        }

        if (!string.IsNullOrEmpty(modalidade))
        {
            query = query.Where(m => m.Modalidade.Contains(modalidade));
        }

        if (!string.IsNullOrEmpty(categoria))
        {
            query = query.Where(m => m.Status_Campanha.Contains(categoria));
        }

        var mesas = query.OrderByDescending(m => m.DataCriacao).ToList();
        
        return View(mesas);
    }

    // Método para busca AJAX (opcional)
    [HttpGet]
    public IActionResult FiltrarMesas(int? generoId, int? sistemaId, string modalidade, string categoria)
    {
        var query = _context.CampanhasMesas
            .Include(c => c.Genero)
            .Include(c => c.Sistema)
            .Include(c => c.Mestre)
            .AsQueryable();

        if (generoId.HasValue && generoId.Value > 0)
        {
            query = query.Where(m => m.ID_Genero == generoId.Value);
        }

        if (sistemaId.HasValue && sistemaId.Value > 0)
        {
            query = query.Where(m => m.ID_Sistema == sistemaId.Value);
        }

        if (!string.IsNullOrEmpty(modalidade))
        {
            query = query.Where(m => m.Modalidade.Contains(modalidade));
        }

        if (!string.IsNullOrEmpty(categoria))
        {
            query = query.Where(m => m.Status_Campanha.Contains(categoria));
        }

        var mesas = query.OrderByDescending(m => m.DataCriacao).ToList();
        
        return PartialView("_MesasCards", mesas);
    }

    public IActionResult MesaDetalhes(int id)
    {
        CampanhaMesa mesa = 
            _context.CampanhasMesas
            .Include(c => c.Genero)
            .Include(c => c.Sistema)
            .Include(c => c.Mestre)
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