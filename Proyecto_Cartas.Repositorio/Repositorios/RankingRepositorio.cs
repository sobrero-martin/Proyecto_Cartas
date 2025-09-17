using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    internal class RankingRepositorio : Repositorio<Ranking>, IRankingRepositorio
    {
        private readonly AppDbContext context;

        public RankingRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Ranking>> GetAllAsync()
        => await context.Set<Ranking>().ToListAsync();

        public async Task<Ranking?> GetByIdAsync(int id)
            => await context.Set<Ranking>().FindAsync(id);

        public async Task AddRango(Ranking rango)
        {
            await context.Set<Ranking>().AddAsync(rango);
            await context.SaveChangesAsync();
        }

        public async Task UpdateRanking(Ranking rango)
        {
            context.Set<Ranking>().Update(rango);
            await context.SaveChangesAsync();
        }
    }
}
