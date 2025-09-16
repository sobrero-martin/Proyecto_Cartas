using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class CartaSobre : EntidadBase
    {
        public int CartaID { get; set; }
        public Carta? Carta { get; set; }
        public int SobreID { get; set; }
        public Sobre? Sobre { get; set; }

        [Required(ErrorMessage = "La probabilidad es requerida.")]
        public int ProbabilidadCartaSobre { get; set; } = 0;
    }
}
