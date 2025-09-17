using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface ICompraSobreRepositorio : IRepositorio<CompraSobre>
    {
        Task<List<CompraSobreMostrarDTO?>> FechaDeCompra(DateTime fecha);
        Task<List<CompraSobreMostrarDTO?>> ListaCompraSobre();
        Task<List<CompraSobreMostrarDTO?>> ListaCompraSobreUsuario(string nombreUsuario);
        Task<List<CompraSobreMostrarDTO?>> ListaQuienComproSobre(string nombreSobre);
    }
}