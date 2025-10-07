using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class CartaAperturaMostrarDTO
    {
        public int Id { get; set; }
        public int CompraSobreID { get; set; }
        public int CartaSobreID { get; set; }
        public int CantidadCartasObtenidas { get; set; }
        public string NombreCarta { get; set; } = "";
        public string NombreSobre { get; set; } = "";
    }
}
