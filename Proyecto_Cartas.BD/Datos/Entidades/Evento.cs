using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.BD.Datos.Entidades
{
    public class Evento : EntidadBase
    {
        public int TurnoID { get; set; }
        public Turno? Turno { get; set; }

        public int EstadoCartaID { get; set; }
        public EstadoCarta? EstadoCarta { get; set; }

        public required string Accion { get; set; }

        public required string Origen { get; set; }

        public string Destino { get; set; } = string.Empty;
    }
}
