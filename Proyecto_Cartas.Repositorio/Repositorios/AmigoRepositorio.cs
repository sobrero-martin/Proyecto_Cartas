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
    public class AmigoRepositorio : Repositorio<Amigo>, IAmigoRepositorio
    {
        private readonly AppDbContext context;

        public AmigoRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Amigo>> GetAllAsync()
        => await context.Set<Amigo>().ToListAsync();

        public async Task<Amigo?> GetByIdAsync(int id)
            => await context.Set<Amigo>().FindAsync(id);

        public async Task<List<Amigo>> GetAmigosPorUsuario(int usuarioId)
        {
            return await context.Amigos
                .Where(a => a.UsuarioId1 == usuarioId)
                .ToListAsync();
        }

        public async Task AddAmigo(Amigo amigo)
        {
            await context.Set<Amigo>().AddAsync(amigo);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAmigo(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                context.Set<Amigo>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
