using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class Turno : EntidadBase
    {
        public int UsuarioPartidaID { get; set; }
        public UsuarioPartida? UsuarioPartida { get; set; }

        public required int Numero { get; set; }

        public required string Fase { get; set; }

    }
}
