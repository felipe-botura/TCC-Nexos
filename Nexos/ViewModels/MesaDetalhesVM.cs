using Nexos.Models;
using System.Collections.Generic;

namespace Nexos.ViewModels
{
    public class MesaDetalhesVM
    {
        public int ID_Campanha { get; set; }
        public string Titulo_Campanha { get; set; }
        public string Premissa_Campanha { get; set; }
        public string O_Que_Esperar { get; set; }
        public string Imagem_Capa { get; set; }
        public string Status_Campanha { get; set; }
        public int Vagas_Disponiveis { get; set; }
        public string Faixa_Etaria { get; set; }
        public string Dias_Horarios { get; set; }
        public string Modalidade { get; set; }
        public string Plataformas { get; set; }
        public string Requisitos { get; set; }
        public int Numero_Jogadores { get; set; }

        // Informações do Mestre
        public string MestreNome { get; set; }
        public string MestreDescricao { get; set; }
        public string MestreFoto { get; set; }

        // Detalhes do Sistema e Gênero
        public string NomeSistema { get; set; }
        public string NomeGenero { get; set; }
    }
}
