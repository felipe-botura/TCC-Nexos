using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    // GET: CampanhasMesas/Create
    [Authorize]
    public IActionResult CriarMesa()
    {
        ViewData["ID_Genero"] = new SelectList(_context.Generos, "ID_Genero", "Nome_Genero");
        ViewData["ID_Sistema"] = new SelectList(_context.Sistemas, "ID_Sistema", "Nome_Sistema");
        return View();
    }

    // POST: CampanhasMesas/Create
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CriarMesa([Bind("ID_Campanha,Titulo_Campanha,ID_Mestre,Premissa_Campanha,O_Que_Esperar,Imagem_Capa,Status_Campanha,Categoria,Vagas_Disponiveis,Faixa_Etaria,ID_Sistema,ID_Genero,Dias_Horarios,Modalidade,Plataformas,Requisitos,Numero_Jogadores,DataCriacao")] CampanhaMesa campanhaMesa, IFormFile imagemCapaFile)
    {
        ModelState.Remove("ID_Mestre");
        ModelState.Remove("Mestre");
        
        if (ModelState.IsValid)
        {
            // Obter o usuário atual e definir como mestre
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            campanhaMesa.ID_Mestre = userId;
            campanhaMesa.DataCriacao = DateTime.Now;

            // A lógica de upload de imagem e salvamento no banco de dados
            if (imagemCapaFile != null && imagemCapaFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "campaigns");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imagemCapaFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagemCapaFile.CopyToAsync(fileStream);
                }
                campanhaMesa.Imagem_Capa = "/img/campaigns/" + uniqueFileName;
            }
            else
            {
                // Define uma imagem padrão se nenhuma for enviada
                campanhaMesa.Imagem_Capa = "/img/campaigns/placeholder1.png";
            }

            _context.Add(campanhaMesa);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Mesa criada com sucesso!";
            return RedirectToAction("Mesas", "Home");
        }
        else
        {
            // Logar erros de validação
            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    _logger.LogError("Erro de validação no campo '{FieldName}': {ErrorMessage}", modelStateKey, error.ErrorMessage);
                }
            }
        }

        // Se houver erros, recarregar os dropdowns
        ViewData["ID_Genero"] = new SelectList(_context.Generos, "ID_Genero", "Nome_Genero", campanhaMesa.ID_Genero);
        ViewData["ID_Sistema"] = new SelectList(_context.Sistemas, "ID_Sistema", "Nome_Sistema", campanhaMesa.ID_Sistema);
        return View(campanhaMesa);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
