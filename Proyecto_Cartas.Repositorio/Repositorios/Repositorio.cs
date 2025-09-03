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

        public async Task<int> Post(E entidad)
        {
            try
            {
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Existe(int id)
        {
            bool existe = await context.Set<E>().AnyAsync(x=>x.Id == id);
            return existe;
        }

        public async Task<bool> Put(int id, E entidad)
        {
            if (id != entidad.Id) return false;

            var existe = await Existe(id);
            if (!existe) return false;

            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> Delete(int id)
        {
            var entidad = await GetById(id);
            if (entidad == null) return false;

            try
            {
                context.Set<E>().Remove(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }
    }
}
