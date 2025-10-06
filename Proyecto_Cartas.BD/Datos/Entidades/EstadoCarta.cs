using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class EstadoCarta : EntidadBase
    {
        public int UsuarioPartidaID { get; set; }
        public UsuarioPartida? UsuarioPartida { get; set; }

        public int InventarioID { get; set; }
        public Inventario? Inventario { get; set; }

        public required int Ataque { get; set; }

        public required int Vida { get; set; }

        public required int Velocidad { get; set; }

        public required string Posicion { get; set; }
    }
}
