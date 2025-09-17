using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class CartaApertura : EntidadBase
    {
        public int CompraSobreID { get; set; }
        public CompraSobre? CompraSobre { get; set; }
        public int CartaSobreID { get; set; }
        public CartaSobre? CartaSobre { get; set; }
        
        [Required(ErrorMessage = "La cantidad obtenida es requerida.")]
        public int CantidadCartasObtenidas { get; set; } = 0;
    }
}
