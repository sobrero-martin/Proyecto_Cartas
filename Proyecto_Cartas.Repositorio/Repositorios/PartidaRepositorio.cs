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
    public class PartidaRepositorio : Repositorio<Partida>, IPartidaRepositorio
    {
        private readonly AppDbContext context;
        public PartidaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Partida?> BuscarPartidaDisponible()
        {
            return await context.Partidas
                .Where(p => p.Estado == "Buscando")
                .FirstOrDefaultAsync();
        }

        public async Task<Partida> CrearPartida()
        {
            var partida = new Partida
            {
                Estado = "Buscando",
                FechaCreacion = DateTime.Now,
                Ganador = 0
            };

            context.Partidas.Add(partida);
            await context.SaveChangesAsync();
            return partida;
        }

        public async Task ActualizarEstadoPartida(int partidaId, string nuevoEstado)
        {
            var partida = await context.Partidas.FindAsync(partidaId);
            if (partida != null)
            {
                partida.Estado = nuevoEstado;
                context.Partidas.Update(partida);
                await context.SaveChangesAsync();
            }
        }
    }
}
