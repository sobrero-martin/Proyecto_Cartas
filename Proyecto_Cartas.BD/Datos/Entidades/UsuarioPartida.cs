using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class UsuarioPartida : EntidadBase
    {
        public int PerfilUsuarioID { get; set; }
        public PerfilUsuario? PerfilUsuario { get; set; }


        public int PartidaID { get; set; }
        public Partida? Partida { get; set; }


        public bool Aceptado { get; set; }

        public int CartasPerdidas { get; set; } = 0;
    }
}
