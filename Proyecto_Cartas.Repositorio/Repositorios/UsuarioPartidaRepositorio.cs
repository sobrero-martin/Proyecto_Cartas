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
                Aceptado = false
            };

            context.UsuariosPartida.Add(usuarioPartida);
            await context.SaveChangesAsync();

            var cantidad = await ContarJugadoresEnPartida(partida.Id);

            if (cantidad >= 2)
            {
                await partidaRepositorio.ActualizarEstadoPartida(partida.Id, "PorConfirmar");
            }
        }

        public async Task<bool> JugadorYaEnPartida(int perfilUsuarioId)
        {
            return await context.UsuariosPartida
                .AnyAsync(p => p.PerfilUsuarioID == perfilUsuarioId && p.Partida!.Estado == "EnProgreso");
        }

        public async Task<int> JugadorPartida(int perfilUsuarioId)
        {
            return await context.UsuariosPartida
                .Where(p => p.PerfilUsuarioID == perfilUsuarioId && p.Partida!.Estado == "EnProgreso")
                .Select(p => p.PartidaID)
                .FirstOrDefaultAsync();
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
                .AnyAsync(p => p.PerfilUsuarioID == perfilUsuarioId && p.Partida != null && (p.Partida.Estado == "Buscando" || p.Partida.Estado =="PorConfirmar"));
        }

        public async Task<bool> CancelarPartida(int perfilUsuarioId)
        {
            var usuarioPartida = await context.UsuariosPartida
                .Include(p => p.Partida)
                .Where(p => p.PerfilUsuarioID == perfilUsuarioId && p.Partida != null && (p.Partida.Estado == "Buscando" || p.Partida.Estado == "EnProgreso" || p.Partida.Estado == "PorConfirmar"))
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
            else if (partida?.Estado == "PorConfirmar")
            {
                context.Partidas.Remove(partida);
                context.UsuariosPartida.Remove(usuarioPartida);
            }

                await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConfirmarPartida(int perfilUsuarioId)
        {
            var usuarioPartida = await context.UsuariosPartida
                                .Include(p => p.Partida)
                                .Where(p => p.PerfilUsuarioID == perfilUsuarioId && p.Partida != null && (p.Partida.Estado == "PorConfirmar"))
                                .FirstOrDefaultAsync();

            if (usuarioPartida == null) return false;

            usuarioPartida.Aceptado = true;
            await context.SaveChangesAsync();
            
            var jugadores = await ObtenerJugadoresEnPartida(usuarioPartida.PartidaID);
            if (jugadores.All(j => j.Aceptado))
            {
                usuarioPartida.Partida!.Estado = "EnProgreso";
                await context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<RevisarEstadoDTO?> RevisarPartidaEncontrada(int perfilUsuarioId)
        {
            var partida = await context.UsuariosPartida
                          .Include(p => p.Partida)
                          .Where(p => p.PerfilUsuarioID == perfilUsuarioId && p.Partida != null && p.Partida.Estado == "PorConfirmar")
                          .FirstOrDefaultAsync();

            if (partida == null)
            {
                return null;
            }

            var perfilUsuarioRivalId = await context.UsuariosPartida
                                        .Where(p => p.PartidaID == partida.PartidaID && p.PerfilUsuarioID != perfilUsuarioId)
                                        .Select(p => p.PerfilUsuarioID)
                                        .FirstOrDefaultAsync();

            return new RevisarEstadoDTO
            {
                PartidaId = partida.PartidaID,
                PerfilUsuarioId = partida.PerfilUsuarioID,

                Nombre = await context.PerfilesUsuario
                          .Where(p => p.Id == perfilUsuarioId)
                          .Select(p => p.Nombre)
                          .FirstOrDefaultAsync() ?? string.Empty,

                PerfilUsuarioRivalId = perfilUsuarioRivalId,

                NombreRival = await context.PerfilesUsuario
                               .Where(p => p.Id == perfilUsuarioRivalId)
                               .Select(p => p.Nombre)
                               .FirstOrDefaultAsync() ?? string.Empty
            };
        }

        public async Task<int?> BuscarPartidaPorJugador(int perfilUsuarioId)
        {
            var partida = await context.UsuariosPartida
                          .Include(p => p.Partida)
                          .Where(p => p.PerfilUsuarioID == perfilUsuarioId && p.Partida != null && (p.Partida.Estado == "EnProgreso"))
                          .FirstOrDefaultAsync();

            return partida?.PartidaID;
        }

        public async Task<int?> BuscarUsuarioPartida(int perfilUsuarioId)
        {
            var usuarioPartida = await context.UsuariosPartida
                                  .Where(up => up.PerfilUsuarioID == perfilUsuarioId && up.Partida != null &&(up.Partida.Estado == "EnProgreso"))
                                  .FirstOrDefaultAsync();

            return usuarioPartida?.Id;
        }

        public async Task<int> BuscarUsuarioPartidaRival(int usuarioPartidaId)
        {
            var usuarioPartida = await context.UsuariosPartida
                                  .Include(up => up.Partida)
                                  .Where(up => up.Id == usuarioPartidaId && up.Partida != null && up.Partida.Estado == "EnProgreso")
                                  .FirstOrDefaultAsync();

            if (usuarioPartida == null)
            {
                return 0;
            }

            var usuarioPartidaRival = await context.UsuariosPartida
                                        .Where(up => up.PartidaID == usuarioPartida!.PartidaID && up.Id != usuarioPartidaId)
                                        .FirstOrDefaultAsync();

            if (usuarioPartidaRival == null)
            {
                return 0;
            }

            return usuarioPartidaRival!.Id;
        }

        public async Task<bool> RevisarDerrota(int usuarioPartidaId)
        {
            var usuarioPartida = await context.UsuariosPartida.FindAsync(usuarioPartidaId);

            return (usuarioPartida!.CartasPerdidas >= 3);    
        }

    }
}

