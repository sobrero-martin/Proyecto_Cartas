using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IUsuarioPartidaRepositorio : IRepositorio<UsuarioPartida>
    {
        Task AgregarJugador(Partida partida, int jugadorId);
        Task<bool> JugadorYaEnPartida(int perfilUsuarioId);
        Task<int> JugadorPartida(int perfilUsuarioId);
        Task<int> ContarJugadoresEnPartida(int partidaId);
        Task<List<UsuarioPartida>> ObtenerJugadoresEnPartida(int partidaId);
        Task<bool> CancelarPartida(int perfilUsuarioId);
        Task<bool> JugadorBuscandoPartida(int perfilUsuarioId);
        Task<bool> ConfirmarPartida(int perfilUsuarioId);
        Task<RevisarEstadoDTO?> RevisarPartidaEncontrada(int perfilUsuarioId);
        Task<int?> BuscarPartidaPorJugador(int perfilUsuarioId);

        Task<int?> BuscarUsuarioPartida(int perfilUsuarioId);
        Task<int> BuscarUsuarioPartidaRival(int usuarioPartidaId);
        Task<bool> RevisarDerrota(int usuarioPartidaId);
    }
}