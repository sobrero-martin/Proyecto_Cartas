using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class RevisarEstadoDTO
    {
        public int PartidaId { get; set; }
        public int PerfilUsuarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int PerfilUsuarioRivalId { get; set; }
        public string NombreRival { get; set; } = string.Empty;
    }
}
