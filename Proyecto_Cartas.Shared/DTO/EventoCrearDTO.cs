using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Shared.DTO
{
    public class EventoCrearDTO
    {
        public int TurnoID { get; set; }

        public int EstadoCartaID { get; set; }

        public required string Accion { get; set; }

        public required string Origen { get; set; }

        public string Destino { get; set; } = string.Empty;
    }
}
