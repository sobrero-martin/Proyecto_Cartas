using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class Ranking : EntidadBase
    {
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }

        public int Puntos { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        public required string Tipo { get; set; }


        public int Posicion { get; set; }
    }
}
