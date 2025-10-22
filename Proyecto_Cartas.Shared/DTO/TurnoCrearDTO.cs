using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class TurnoCrearDTO
    {
        public int UsuarioPartidaID { get; set; }

        public required int Numero { get; set; }

        public required string Fase { get; set; }
    }
}
