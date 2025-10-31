using Nexos.Models;
using System.Collections.Generic;

namespace Nexos.Controllers
{
    public class ChatCampanhaViewModel
    {
        public CampanhaMesa Campanha { get; set; }
        public bool IsMestre { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<ChatCampanha> Mensagens { get; set; }
    }
}
