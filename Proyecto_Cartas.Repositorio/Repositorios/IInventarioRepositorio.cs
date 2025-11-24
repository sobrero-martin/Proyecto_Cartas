using Proyecto_Cartas.BD.Datos.Entidades;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IInventarioRepositorio : IRepositorio<Inventario>
    {
        Task<bool> MazoInicial(int usuarioId, int opcion);
    }
}