using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class CompraSobreMostrarDTO
    {
        public int PerfilUsuarioID { get; set; }
        public string NombreSobre { get; set; } = null!;
        public DateTime FechaCompra { get; set; }
    }
}
