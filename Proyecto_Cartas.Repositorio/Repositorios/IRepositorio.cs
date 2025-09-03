using Proyecto_Cartas.BD.Datos;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IRepositorio<E> where E : class, IEntidadBase
    {
        Task<List<E>> GetFull();
        Task<E?> GetById(int id);
    }
}