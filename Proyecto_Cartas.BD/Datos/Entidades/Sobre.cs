using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class Sobre : EntidadBase
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [MaxLength(45, ErrorMessage = "El nombre del sobre no puede exceder los 45 caracteres.")]
        public required string NombreSobre { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida.")]
        public int CantidadCartas { get; set; } = 0;
        }
}
