using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexos.Data;
using Nexos.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Nexos.ViewModels;

namespace Nexos.Controllers
{
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ChatController(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Campanha(int id)
        {
            var campanha = await _context.CampanhasMesas
                .Include(c => c.Mestre)
                .FirstOrDefaultAsync(c => c.ID_Campanha == id);

            if (campanha == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isMestre = campanha.ID_Mestre == userId;

            // Lógica para verificar se o usuário é jogador (simplesmente se não for o mestre)
            // Em um sistema real, você teria uma tabela de relacionamento Campanha-Jogador
            // Por enquanto, vamos considerar que qualquer usuário logado que não seja o mestre é um "jogador"
            // que tem acesso ao chat (o que pode ser ajustado depois).
            var isJogador = !isMestre && userId != null;
            
            var currentUser = await _userManager.FindByIdAsync(userId);
            var currentUserName = currentUser?.Nome ?? "Usuário Desconhecido";

            if (!isMestre && !isJogador)
            {
                // Usuário não logado ou sem permissão
                return Forbid();
            }

            var chatViewModel = new ChatCampanhaViewModel
            {
                Campanha = campanha,
                IsMestre = isMestre,
                UserId = userId,
                UserName = currentUserName,
                // Carregar as 50 últimas mensagens
                Mensagens = await _context.ChatsCampanha
                    .Where(c => c.IdCampanha == id)
                    .Include(c => c.User)
                    .OrderByDescending(c => c.DataHora)
                    .Take(50)
                    .OrderBy(c => c.DataHora)
                    .ToListAsync()
            };

            return View(chatViewModel);
        }
    }
}
