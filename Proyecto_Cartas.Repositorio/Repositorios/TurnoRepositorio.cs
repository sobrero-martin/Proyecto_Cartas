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
    }
}
