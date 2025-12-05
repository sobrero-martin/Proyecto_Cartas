using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public class TurnoRepositorio : Repositorio<Turno>, ITurnoRepositorio
    {
        private readonly AppDbContext context;

        public TurnoRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<TurnoDTO> CrearTurno(TurnoCrearDTO turnoDTO)
        {
            var turno = new Turno
            {
                
                UsuarioPartidaID = turnoDTO.UsuarioPartidaID,
                Numero = turnoDTO.Numero,
                Fase = turnoDTO.Fase
            };

            context.Turnos.Add(turno);

            await context.SaveChangesAsync();

            return new TurnoDTO
            {
                Id = turno.Id,
                UsuarioPartidaID = turno.UsuarioPartidaID,
                Numero = turno.Numero,
                Fase = turno.Fase
            };
        }

        public async Task<TurnoDTO> UltimoTurno(int usuarioPartidaId)
        {
           

            var turno = await context.Turnos
                .Where(t => t.UsuarioPartidaID == usuarioPartidaId)
                .OrderByDescending(t => t.Id)
                .Select(t => new TurnoDTO
                {
                    Id = t.Id,
                    UsuarioPartidaID = t.UsuarioPartidaID,
                    Numero = t.Numero,
                    Fase = t.Fase
                })
                .FirstOrDefaultAsync();

            return turno ?? throw new Exception("No se encontró turno");
        }

        public async Task<bool> ExisteRoboInicial(int usuarioPartidaId)
        {
            return await context.Turnos
                .AnyAsync(t => t.UsuarioPartidaID == usuarioPartidaId && t.Fase=="Robo Inicial")
                ;

        }
    }
}
