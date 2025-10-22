using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IEstadoCartaRepositorio : IRepositorio<EstadoCarta>
    {
        Task<EstadoCartaDTO?> CambiarPosicion(int id, string nuevaPosicion, int turnoId, string accion);
        Task<EstadoCartaDTO?> ColocarEnCampo(int usuarioPartidaId, int cartaId, int lugar, int turnoId);
        Task<EstadoCartaDTO?> EnviarAlCementerio(int usuarioPartidaId, int cartaId, int turnoId);
        Task<List<EstadoCartaDTO>> EstadoCartaDeUnUsuario(int usuarioPartidaId);
        Task<List<EstadoCartaDTO>> FiltrarPosicion(int usuarioPartidaId, string posicion);
        Task<EstadoCartaDTO?> RobarCarta(int usuarioPartidaId, int turnoId);
        Task<List<EstadoCartaDTO>> RobarCartasCantidad(int usuarioPartidaId, int cantidad, int turnoId);
    }
}