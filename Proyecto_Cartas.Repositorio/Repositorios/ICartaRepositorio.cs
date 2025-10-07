using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface ICartaRepositorio : IRepositorio<Carta>
    {
        Task<List<CartaNumeroDTO?>> ListaATKCarta();
        Task<List<CartaNumeroDTO?>> ListaNivelCarta();
        Task<List<CartaDTO?>> ListaNombreCarta();
        Task<List<CartaPalabraDTO?>> ListaTipoCarta();
        Task<List<CartaNumeroDTO?>> ListaVelocidadCarta();
        Task<List<CartaNumeroDTO?>> ListaVIDACarta();
        Task<List<CartaDTO?>> NivelCarta(int nivel);
        Task<CartaDTO?> NombreCarta(string nombre);
        Task<List<CartaDTO?>> TipoCarta(string tipo);
    }
}