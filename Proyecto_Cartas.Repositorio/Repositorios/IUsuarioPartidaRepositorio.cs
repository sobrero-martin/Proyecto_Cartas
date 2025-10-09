using Proyecto_Cartas.BD.Datos.Entidades;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IUsuarioPartidaRepositorio : IRepositorio<UsuarioPartida>
    {
        Task AgregarJugador(Partida partida, int jugadorId);
        Task<bool> JugadorYaEnPartida(int perfilUsuarioId);
        Task<int> ContarJugadoresEnPartida(int partidaId);
        Task<List<UsuarioPartida>> ObtenerJugadoresEnPartida(int partidaId);
        Task<bool> CancelarPartida(int perfilUsuarioId);
        Task<bool> JugadorBuscandoPartida(int perfilUsuarioId);
    }
}