using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class Carta : EntidadBase
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [MaxLength(45, ErrorMessage = "El nombre no puede exceder los 45 caracteres")]
        public required string NombreCarta { get; set; }

        [Required(ErrorMessage = "El tipo de carta es requerido.")]
        [MaxLength(45, ErrorMessage = "El tipo de carta no puede exceder los 45 caracteres")]
        public string TipoCarta { get; set; } = "";

        [Required(ErrorMessage = "El nivel es requerido.")]
        public int NivelCarta { get; set; } = 1;

        [Required(ErrorMessage = "El ataque es requerido.")]
        public int Ataque { get; set; } = 0;

        [Required(ErrorMessage = "La vida es requerida.")]
        public int Vida { get; set; } = 0;

        [Required(ErrorMessage = "La velocidad es requerida.")]
        public int Velocidad { get; set; } = 0;
    }
}
