using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class UsuarioPartida : EntidadBase
    {
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }


        public int PartidaID { get; set; }
        public Partida? Partida { get; set; }


        public bool Aceptado { get; set; }
    }
}
