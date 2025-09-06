using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IRepositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly AppDbContext context;

        public UsuarioRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Usuario?> GetByEmail(string email)
        {
            return await context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<UsuarioListadoDTO>> GetListado()
        {
            return await context.Usuarios.Select(u => new UsuarioListadoDTO
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email
                })
                .ToListAsync();
        }
    }
}
