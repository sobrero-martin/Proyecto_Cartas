using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class Repositorio<E> : IRepositorio<E> where E : class, IEntidadBase
    {
        private readonly AppDbContext context;
        public Repositorio(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<E>> GetFull()
        {
            return await context.Set<E>().ToListAsync();
        }

        public async Task<E?> GetById(int id)
        {
            return await context.Set<E>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
