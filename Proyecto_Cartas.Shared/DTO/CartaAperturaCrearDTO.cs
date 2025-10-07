using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class CartaAperturaCrearDTO
    {
        public int CompraSobreID { get; set; }
        public int CartaSobreID { get; set; }

        [Required(ErrorMessage = "La cantidad obtenida es requerida.")]
        public int CantidadCartasObtenidas { get; set; }
    }
}

