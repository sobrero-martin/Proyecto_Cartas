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
    public class ConfiguracionUsuarioRepositorio : Repositorio<ConfiguracionUsuario>, IConfiguracionUsuarioRepositorio
    {
        private readonly AppDbContext context;

        public ConfiguracionUsuarioRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<ConfiguracionUsuarioDTO?> GetByUsuarioId(int usuarioId)
        {
            return await context.ConfiguracionesUsuario
                .Where(c => c.UsuarioID == usuarioId)
                .Select(c => new ConfiguracionUsuarioDTO
                {
                    UsuarioID = c.UsuarioID,
                    VolumenMusica = c.VolumenMusica,
                    VolumenSFX = c.VolumenSFX,
                })
                .FirstOrDefaultAsync();
        }
    }
}
