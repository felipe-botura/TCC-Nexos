using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Nexos.Controllers
{
    public class MesasController : Controller
    {
        private readonly ILogger<MesasController> _logger;

        public MesasController(ILogger<MesasController> logger)
        {
            _logger = logger;
        }

        // GET: /Mesas
        public IActionResult Index()
        {
            ViewData["Title"] = "Mesas Disponíveis";

            // procura Views/Mesas/Mesas.cshtml (ou Index.cshtml, depende do nome que você usou)
            return View("Mesas");
        }

        // Exemplo de detalhes de uma mesa (futuro)
        public IActionResult MesaDetalhes(int id)
        {
            ViewData["Title"] = "Detalhes da Mesa";
            return View();
        }
    }
}
