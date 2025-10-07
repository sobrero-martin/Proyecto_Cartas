using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface ICartaSobreRepositorio : IRepositorio<CartaSobre>
    {
        Task<List<CartaSobreDTO?>> ListaCartaSobre();
        Task<List<CartaSobreDTO?>> ListaCartaSobreNombre(string nombreSobre);
        Task<List<CartaSobreDTO?>> ListaCartaSobreNombreCarta(string nombreCarta);
    }
}