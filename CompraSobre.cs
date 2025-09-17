using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class CompraSobre : EntidadBase
    {
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }
        public int SobreID { get; set; }
        public Sobre? Sobre { get; set; }

        [Required(ErrorMessage = "La fecha de compra es requerida.")]
        public DateTime FechaCompra { get; set; } = DateTime.UtcNow;
    }
}
