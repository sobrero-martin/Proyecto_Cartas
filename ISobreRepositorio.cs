using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface ISobreRepositorio : IRepositorio<Sobre>
    {
        Task<List<SobreDTO?>> ListaNombreSobre();
        Task<SobreDTO?> NombreSobre(string nombre);
    }
}