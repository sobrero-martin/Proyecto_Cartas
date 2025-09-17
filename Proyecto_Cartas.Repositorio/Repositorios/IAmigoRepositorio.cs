using Proyecto_Cartas.BD.Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IAmigoRepositorio : IRepositorio<Amigo>
    {
        Task AddAmigo(Amigo amigo);
        Task DeleteAmigo(int id);
        Task<IEnumerable<Amigo>> GetAllAsync();
        Task<List<Amigo>> GetAmigosPorUsuario(int usuarioId);
        Task<Amigo?> GetByIdAsync(int id);
    }
}
