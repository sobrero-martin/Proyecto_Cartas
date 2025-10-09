using Proyecto_Cartas.BD.Datos.Entidades;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IPartidaRepositorio : IRepositorio<Partida>
    {
        Task<Partida?> BuscarPartidaDisponible();
        Task<Partida> CrearPartida();
        Task ActualizarEstadoPartida(int partidaId, string nuevoEstado);
       
    }
}