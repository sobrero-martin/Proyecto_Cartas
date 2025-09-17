using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class Inventario : EntidadBase
    {
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }

        public int CartaID { get; set; }
        public Carta? Carta { get; set; }
    }
}
