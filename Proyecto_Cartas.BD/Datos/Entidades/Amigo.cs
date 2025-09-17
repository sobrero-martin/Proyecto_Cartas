using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class Amigo : EntidadBase
    {
        public int UsuarioId1 { get; set; }
        public Usuario? Usuario1 { get; set; }


        public int UsuarioId2 { get; set; }
        public Usuario? Usuario2 { get; set; }


        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public required string Estado { get; set; }
    }
}
