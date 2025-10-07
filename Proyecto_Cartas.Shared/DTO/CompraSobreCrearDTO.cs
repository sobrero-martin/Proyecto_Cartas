using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class CompraSobreCrearDTO
    {
        public int Id { get; set; }
        public int PerfilUsuarioID { get; set; }
        public int SobreID { get; set; }
        
        [Required(ErrorMessage = "La fecha de compra es requerida.")]
        public DateTime FechaCompra { get; set; } = DateTime.UtcNow;
    }
}
