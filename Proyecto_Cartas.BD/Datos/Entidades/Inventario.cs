using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class Inventario : EntidadBase
    {
        public int PerfilUsuarioID { get; set; }
        public PerfilUsuario? PerfilUsuario { get; set; }

        public int CartaID { get; set; }
        public Carta? Carta { get; set; }
        public required string Tipo { get; set; }
    }
}
