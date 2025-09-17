using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class PartidaCreateDTO
    {
        public int UsuarioID1 { get; set; }
        public int UsuarioID2 { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Ganador { get; set; } = string.Empty;
    }
}
