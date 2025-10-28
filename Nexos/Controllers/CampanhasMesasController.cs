using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nexos.Data;
using Nexos.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Nexos.Controllers
{
    public class CampanhasMesasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CampanhasMesasController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: CampanhasMesas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CampanhasMesas
                                            .Include(c => c.Genero)
                                            .Include(c => c.Mestre)
                                            .Include(c => c.Sistema);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CampanhasMesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campanhaMesa = await _context.CampanhasMesas
                .Include(c => c.Genero)
                .Include(c => c.Mestre)
                .Include(c => c.Sistema)
                .FirstOrDefaultAsync(m => m.ID_Campanha == id);
            if (campanhaMesa == null)
            {
                return NotFound();
            }

            return View(campanhaMesa);
        }

        // GET: CampanhasMesas/Create
        public IActionResult Create()
        {
            ViewData["ID_Genero"] = new SelectList(_context.Generos, "ID_Genero", "Nome_Genero");
            ViewData["ID_Mestre"] = new SelectList(_context.Users, "Id", "UserName"); // Assumindo que Usuarios é Users
            ViewData["ID_Sistema"] = new SelectList(_context.Sistemas, "ID_Sistema", "Nome_Sistema");
            return View();
        }

        // POST: CampanhasMesas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Campanha,Titulo_Campanha,ID_Mestre,Premissa_Campanha,O_Que_Esperar,Imagem_Capa,Status_Campanha,Vagas_Disponiveis,Faixa_Etaria,ID_Sistema,ID_Genero,Dias_Horarios,Modalidade,Plataformas,Requisitos,Numero_Jogadores,DataCriacao")] CampanhaMesa campanhaMesa, IFormFile ImagemUpload)
        {
            if (ModelState.IsValid)
            {
                // Processar upload da imagem
                if (ImagemUpload != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(ImagemUpload.FileName);
                    string extension = Path.GetExtension(ImagemUpload.FileName);
                    campanhaMesa.Imagem_Capa = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/img/campaigns/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await ImagemUpload.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    // Se nenhuma imagem foi enviada, usar a imagem placeholder
                    campanhaMesa.Imagem_Capa = "placeholder1.png";
                }

                campanhaMesa.DataCriacao = DateTime.Now; // Definir a data de criação automaticamente
                _context.Add(campanhaMesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = campanhaMesa.ID_Campanha });
            }
            ViewData["ID_Genero"] = new SelectList(_context.Generos, "ID_Genero", "Nome_Genero", campanhaMesa.ID_Genero);
            ViewData["ID_Mestre"] = new SelectList(_context.Users, "Id", "UserName", campanhaMesa.ID_Mestre);
            ViewData["ID_Sistema"] = new SelectList(_context.Sistemas, "ID_Sistema", "Nome_Sistema", campanhaMesa.ID_Sistema);
            return View(campanhaMesa);
        }

        // GET: CampanhasMesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campanhaMesa = await _context.CampanhasMesas.FindAsync(id);
            if (campanhaMesa == null)
            {
                return NotFound();
            }
            ViewData["ID_Genero"] = new SelectList(_context.Generos, "ID_Genero", "Nome_Genero", campanhaMesa.ID_Genero);
            ViewData["ID_Mestre"] = new SelectList(_context.Users, "Id", "UserName", campanhaMesa.ID_Mestre);
            ViewData["ID_Sistema"] = new SelectList(_context.Sistemas, "ID_Sistema", "Nome_Sistema", campanhaMesa.ID_Sistema);
            return View(campanhaMesa);
        }

        // POST: CampanhasMesas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Campanha,Titulo_Campanha,ID_Mestre,Premissa_Campanha,O_Que_Esperar,Imagem_Capa,Status_Campanha,Vagas_Disponiveis,Faixa_Etaria,ID_Sistema,ID_Genero,Dias_Horarios,Modalidade,Plataformas,Requisitos,Numero_Jogadores,DataCriacao")] CampanhaMesa campanhaMesa)
        {
            if (id != campanhaMesa.ID_Campanha)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campanhaMesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampanhaMesaExists(campanhaMesa.ID_Campanha))
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
            ViewData["ID_Genero"] = new SelectList(_context.Generos, "ID_Genero", "Nome_Genero", campanhaMesa.ID_Genero);
            ViewData["ID_Mestre"] = new SelectList(_context.Users, "Id", "UserName", campanhaMesa.ID_Mestre);
            ViewData["ID_Sistema"] = new SelectList(_context.Sistemas, "ID_Sistema", "Nome_Sistema", campanhaMesa.ID_Sistema);
            return View(campanhaMesa);
        }

        // GET: CampanhasMesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campanhaMesa = await _context.CampanhasMesas
                .Include(c => c.Genero)
                .Include(c => c.Mestre)
                .Include(c => c.Sistema)
                .FirstOrDefaultAsync(m => m.ID_Campanha == id);
            if (campanhaMesa == null)
            {
                return NotFound();
            }

            return View(campanhaMesa);
        }

        // POST: CampanhasMesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campanhaMesa = await _context.CampanhasMesas.FindAsync(id);
            if (campanhaMesa != null)
            {
                _context.CampanhasMesas.Remove(campanhaMesa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampanhaMesaExists(int id)
        {
            return _context.CampanhasMesas.Any(e => e.ID_Campanha == id);
        }
    }
}

