using Proyecto_Cartas.BD.Datos.Entidades;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IRankingRepositorio : IRepositorio<Ranking>
    {
        Task AddRango(Ranking rango);
        Task<IEnumerable<Ranking>> GetAllAsync();
        Task<Ranking?> GetByIdAsync(int id);
        Task UpdateRanking(Ranking rango);
    }
}