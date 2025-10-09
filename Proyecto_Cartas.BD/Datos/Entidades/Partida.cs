using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class Partida : EntidadBase
    {
        [Required(ErrorMessage = "El estado es obligatorio.")]
        public required string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? Ganador { get; set; }
    }
}
