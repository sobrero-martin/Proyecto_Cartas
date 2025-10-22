using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class TurnoDTO
    {
        public int Id { get; set; }
        public int UsuarioPartidaID { get; set; }
        public int Numero { get; set; }
        public string Fase { get; set; } = string.Empty;
    }
}
