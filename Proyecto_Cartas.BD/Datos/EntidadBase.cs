using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Cartas.Shared.ENUM;

namespace Proyecto_Cartas.BD.Datos
{
    public class EntidadBase : IEntidadBase
    {
        public int Id { get; set; }
        public EnumEstadoRegistro EstadoRegistro { get; set; } = EnumEstadoRegistro.Activo;
    }
}
