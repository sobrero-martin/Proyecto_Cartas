using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class InventarioDTO
    {
        public int PerfilUsuarioId { get; set; }
        public int CartaId { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
