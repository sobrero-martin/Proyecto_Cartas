using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class UsuarioPartidaRepositorio : Repositorio<UsuarioPartida>, IUsuarioPartidaRepositorio
    {
        private readonly AppDbContext context;
        private readonly IPartidaRepositorio partidaRepositorio;

        public UsuarioPartidaRepositorio(AppDbContext context, IPartidaRepositorio partidaRepositorio) : base(context)
        {
            this.context = context;
            this.partidaRepositorio = partidaRepositorio;
        }

        public async Task AgregarJugador(Partida partida, int jugadorId)
        {
            var usuarioPartida = new UsuarioPartida
            {
                PartidaID = partida.Id,
                PerfilUsuarioID = jugadorId,
                CartasPerdidas = 0,
                Aceptado = true
            };

            context.UsuariosPartida.Add(usuarioPartida);
            await context.SaveChangesAsync();

            var cantidad = await ContarJugadoresEnPartida(partida.Id);

            if (cantidad >= 2)
            {
                await partidaRepositorio.ActualizarEstadoPartida(partida.Id, "EnProgreso");
            }
        }

        public async Task<bool> JugadorYaEnPartida(int perfilUsuarioId)
        {
            return await context.UsuariosPartida
                .AnyAsync(p => p.PerfilUsuarioID == perfilUsuarioId && p.Partida!.Estado == "EnProgreso");
        }

        public async Task<int> ContarJugadoresEnPartida(int partidaId)
        {
            return await context.UsuariosPartida
                .CountAsync(p => p.PartidaID == partidaId);
        }

        public async Task<List<UsuarioPartida>> ObtenerJugadoresEnPartida(int partidaId)
        {
            return await context.UsuariosPartida
                .Where(p => p.PartidaID == partidaId)
                .ToListAsync();
        }

        public async Task<bool> JugadorBuscandoPartida(int perfilUsuarioId)
        {
            return await context.UsuariosPartida
                .AnyAsync(p => p.PerfilUsuarioID == perfilUsuarioId && p.Partida != null && p.Partida.Estado == "Buscando");
        }

        public async Task<bool> CancelarPartida(int perfilUsuarioId)
        {
            var usuarioPartida = await context.UsuariosPartida
                .Include(p => p.Partida)
                .Where(p => p.PerfilUsuarioID == perfilUsuarioId && p.Partida != null && (p.Partida.Estado == "Buscando" || p.Partida.Estado == "EnProgreso"))
                .FirstOrDefaultAsync();

            if (usuarioPartida == null)
                return false;

            var partida = usuarioPartida.Partida;

            if (partida?.Estado == "Buscando")
            {
                context.Partidas.Remove(partida);
                context.UsuariosPartida.Remove(usuarioPartida);
            }
            else if (partida?.Estado == "EnProgreso")
            {
                var rival = await context.UsuariosPartida
                    .FirstOrDefaultAsync(p => p.PartidaID == partida.Id && p.PerfilUsuarioID != perfilUsuarioId);

                partida.Estado = "Finalizada";
                partida.Ganador = rival?.PerfilUsuarioID;
            }

            await context.SaveChangesAsync();
            return true;
        }
    }
}

