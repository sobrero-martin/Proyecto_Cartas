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
    public class EventoRepositorio : Repositorio<Evento>, IEventoRepositorio
    {
        private readonly AppDbContext context;

        public EventoRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Evento> CrearEvento(EventoCrearDTO eventoDto)
        {
            var existeTurno = await context.Turnos.AnyAsync(t => t.Id == eventoDto.TurnoID);
            var existeCarta = await context.EstadosCarta.AnyAsync(ec => ec.Id == eventoDto.EstadoCartaID);

            if (!existeTurno || !existeCarta)
            {
                throw new Exception("El TurnoID o EstadoCartaID no existen.");
            }
            var evento = new Evento
            {
                TurnoID = eventoDto.TurnoID,
                EstadoCartaID = eventoDto.EstadoCartaID,
                Accion = eventoDto.Accion,
                Origen = eventoDto.Origen,
                Destino = eventoDto.Destino
            };

            context.Eventos.Add(evento);
            await context.SaveChangesAsync();
            return evento;

        }
    }
}
