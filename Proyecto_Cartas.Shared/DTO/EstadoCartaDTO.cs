using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class EstadoCartaDTO
    {
        public int Id { get; set; }
        public int UsuarioPartidaID { get; set; }
        public int InventarioID { get; set; }
        public string Nombre { get; set; } = "";
        public int Ataque { get; set; }
        public int Vida { get; set; }
        public int Velocidad { get; set; }
        public string Posicion { get; set; } = "";
    }
}
