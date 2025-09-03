using Proyecto_Cartas.Shared.ENUM;

namespace Proyecto_Cartas.BD.Datos
{
    public interface IEntidadBase
    {
        EnumEstadoRegistro EstadoRegistro { get; set; }
        int Id { get; set; }
    }
}