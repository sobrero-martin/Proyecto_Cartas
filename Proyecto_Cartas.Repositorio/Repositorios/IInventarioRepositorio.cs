using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IInventarioRepositorio : IRepositorio<Inventario>
    {
        Task<bool> MazoInicial(int usuarioId, int opcion);
        Task<List<InventarioDTO>> GetByPerfilUsuarioId(int perfilUsuarioId);
    }
}