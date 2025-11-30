using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IEstadoCartaRepositorio : IRepositorio<EstadoCarta>
    {
        Task<int> CrearEstadosCartas(int usuarioPartidaId, string tipoMazo);
        Task<List<Evento>> Batalla(int idPartida, int turnoId);
        Task<EstadoCartaDTO?> CambiarPosicion(int id, string nuevaPosicion, int turnoId, string accion);
        Task<List<EstadoCartaDTO>> CartasEnCampo(int usuarioPartidaId);
        Task<EstadoCartaDTO?> ColocarEnCampo(int usuarioPartidaId, int cartaId, string lugar, int turnoId);
        Task<EstadoCartaDTO?> EnviarAlCementerio(int usuarioPartidaId, int cartaId, int turnoId);
        Task<List<EstadoCartaDTO>> EstadoCartaDeUnUsuario(int usuarioPartidaId);
        Task<List<EstadoCartaDTO>> FiltrarPosicion(int usuarioPartidaId, string posicion);
        Task<List<EstadoCartaDTO>> ObtenerCartasEnCampo(int idPartida);
        Task<EstadoCartaDTO?> RobarCarta(int usuarioPartidaId, int turnoId);
        Task<List<EstadoCartaDTO>> RobarCartasCantidad(int usuarioPartidaId, int cantidad, int turnoId);
    }
}