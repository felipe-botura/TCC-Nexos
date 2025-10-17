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

[HttpGet("Home/Mesas")]
public IActionResult Mesas(int? generoId, int? sistemaId, string modalidade, string categoria, int page = 1, int pageSize = 9)
{
    ViewData["Title"] = "Mesas Disponíveis";
    ViewBag.Generos = _context.Generos.ToList();
    ViewBag.Sistemas = _context.Sistemas.ToList();

    var query = _context.CampanhasMesas
        .Include(c => c.Genero)
        .Include(c => c.Sistema)
        .Include(c => c.Mestre)
        .AsQueryable();

    if (generoId.HasValue && generoId.Value > 0)
        query = query.Where(m => m.ID_Genero == generoId.Value);

    if (sistemaId.HasValue && sistemaId.Value > 0)
        query = query.Where(m => m.ID_Sistema == sistemaId.Value);

    if (!string.IsNullOrEmpty(modalidade))
        query = query.Where(m => m.Modalidade.Contains(modalidade));

    if (!string.IsNullOrEmpty(categoria))
        query = query.Where(m => m.Status_Campanha.Contains(categoria));

    var total = query.Count();

    var mesas = query
        .OrderByDescending(m => m.DataCriacao)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

    ViewBag.TotalMesas = total;
    ViewBag.PageSize = pageSize;

    return View(mesas);
}

[HttpGet]
public IActionResult CarregarMaisMesas(int page, int pageSize = 9, int? generoId = null, int? sistemaId = null, string modalidade = null, string categoria = null)
{
    var query = _context.CampanhasMesas
        .Include(c => c.Genero)
        .Include(c => c.Sistema)
        .Include(c => c.Mestre)
        .AsQueryable();

    if (generoId.HasValue && generoId.Value > 0)
        query = query.Where(m => m.ID_Genero == generoId.Value);

    if (sistemaId.HasValue && sistemaId.Value > 0)
        query = query.Where(m => m.ID_Sistema == sistemaId.Value);

    if (!string.IsNullOrEmpty(modalidade))
        query = query.Where(m => m.Modalidade.Contains(modalidade));

    if (!string.IsNullOrEmpty(categoria))
        query = query.Where(m => m.Status_Campanha.Contains(categoria));

    var mesas = query
        .OrderByDescending(m => m.DataCriacao)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

    return PartialView("_MesasCards", mesas);
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