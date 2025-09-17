using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class AmigoDTO
    {
        public int UsuarioId2 { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
