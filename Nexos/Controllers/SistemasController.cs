using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexos.Data;
using Nexos.Models;

namespace Nexos.Controllers
{
    public class SistemasController : Controller
    {
        private readonly AppDbContext _context;

        public SistemasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Sistemas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sistemas.ToListAsync());
        }

        // GET: Sistemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sistemas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome_Sistema")] Sistema sistema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sistema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sistema);
        }

        // GET: Sistemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistemas.FindAsync(id);
            if (sistema == null)
            {
                return NotFound();
            }
            return View(sistema);
        }

        // POST: Sistemas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Sistema,Nome_Sistema")] Sistema sistema)
        {
            if (id != sistema.ID_Sistema)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sistema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SistemaExists(sistema.ID_Sistema))
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
            return View(sistema);
        }

        // GET: Sistemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistemas
                .FirstOrDefaultAsync(m => m.ID_Sistema == id);
            if (sistema == null)
            {
                return NotFound();
            }

            return View(sistema);
        }

        // POST: Sistemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sistema = await _context.Sistemas.FindAsync(id);
            if (sistema != null)
            {
                _context.Sistemas.Remove(sistema);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SistemaExists(int id)
        {
            return _context.Sistemas.Any(e => e.ID_Sistema == id);
        }
    }
}

