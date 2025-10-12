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
    public class PerfilUsuarioRepositorio : Repositorio<PerfilUsuario>, IPerfilUsuarioRepositorio
    {

        private readonly AppDbContext context;

        public PerfilUsuarioRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<PerfilUsuarioDTO?> GetPerfilByName(string nombre)
        {
            return await context.PerfilesUsuario
                .Where(p => p.Nombre == nombre)
                .Select(p => new PerfilUsuarioDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Bio = p.Bio,
                    Nivel = p.Nivel,
                    Experiencia = p.Experiencia,
                    PartidasJugadas = p.PartidasJugadas,
                    PartidasGanadas = p.PartidasGanadas,
                    UltimoLogin = p.UltimoLogin.ToString("yyyy-MM-dd HH:mm:ss")
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PerfilUsuarioDTO?> GetPerfilByUserId(int id)
        {
            return await context.PerfilesUsuario
                .Where(p => p.UsuarioID== id)
                .Select(p => new PerfilUsuarioDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Bio = p.Bio,
                    Nivel = p.Nivel,
                    Experiencia = p.Experiencia,
                    PartidasJugadas = p.PartidasJugadas,
                    PartidasGanadas = p.PartidasGanadas,
                    UltimoLogin = p.UltimoLogin.ToString("yyyy-MM-dd HH:mm:ss")
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PerfilUsuarioDTO?> GetPerfilById(int id)
        {
            return await context.PerfilesUsuario
                .Where(p => p.Id== id)
                .Select(p => new PerfilUsuarioDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Bio = p.Bio,
                    Nivel = p.Nivel,
                    Experiencia = p.Experiencia,
                    PartidasJugadas = p.PartidasJugadas,
                    PartidasGanadas = p.PartidasGanadas,
                    UltimoLogin = p.UltimoLogin.ToString("yyyy-MM-dd HH:mm:ss")
                })
                .FirstOrDefaultAsync();
        }


    }
}
