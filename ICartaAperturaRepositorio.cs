using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface ICartaAperturaRepositorio : IRepositorio<CartaApertura>
    {
        Task<CartaAperturaMostrarDTO?> GetAperturaId(int id);
        Task<List<CartaAperturaMostrarDTO>> GetListaApertura();
    }
}