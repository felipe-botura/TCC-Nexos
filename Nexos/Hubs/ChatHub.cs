using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Nexos.Models;
using Nexos.Data;
using System;

namespace Nexos.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(int campanhaId, string userId, string message, int tipoUsuario)
        {
            // 1. Salvar a mensagem no banco de dados
            var chatMessage = new ChatCampanha
            {
                IdCampanha = campanhaId,
                UserId = userId,
                Texto = message,
                DataHora = DateTime.Now,
                TipoUsuario = tipoUsuario // 0=Jogador, 1=Mestre
            };

            _context.ChatsCampanha.Add(chatMessage);
            await _context.SaveChangesAsync();

            // 2. Enviar a mensagem para todos os clientes conectados ao grupo da campanha
            // O nome do grupo será "Campanha_{campanhaId}"
            var user = await _context.Users.FindAsync(userId);
            var userName = user?.Nome ?? "Desconhecido";

            await Clients.Group($"Campanha_{campanhaId}").SendAsync("ReceiveMessage", userId, message, chatMessage.DataHora.ToString("dd/MM HH:mm"), tipoUsuario, userName);
        }

        public async Task JoinCampanha(int campanhaId)
        {
            // Adicionar o usuário ao grupo da campanha
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Campanha_{campanhaId}");
        }

        public async Task LeaveCampanha(int campanhaId)
        {
            // Remover o usuário do grupo da campanha
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Campanha_{campanhaId}");
        }
    }
}
