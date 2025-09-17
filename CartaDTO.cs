using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class CartaDTO
    {
        public int Id { get; set; }
        public string NombreCarta { get; set; } = "";
        public string TipoCarta { get; set; } = "";
        public int NivelCarta { get; set; }
        public int Ataque { get; set; }
        public int Vida { get; set; }
        public int Velocidad { get; set; }
    }
}
