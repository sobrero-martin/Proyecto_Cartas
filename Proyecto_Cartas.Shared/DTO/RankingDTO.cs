using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class RankingDTO
    {
        public int UsuarioId { get; set; }
        public int Puntos { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int Posicion { get; set; }
    }
}
