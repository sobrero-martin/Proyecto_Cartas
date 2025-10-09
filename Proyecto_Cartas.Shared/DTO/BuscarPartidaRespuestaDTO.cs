using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class BuscarPartidaRespuestaDTO
    {
        public bool Aceptado { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public int PartidaId { get; set; }
        public int? PerfilUsuarioRivalId { get; set; }
    }
}
