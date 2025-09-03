using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class Billetera : EntidadBase
    {
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "La cantidad de monedas es requerida.")]
        public int cantidadMonedas { get; set; } = 0;
    }
}
