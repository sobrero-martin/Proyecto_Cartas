using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface IEventoRepositorio : IRepositorio<Evento>
    {
        Task<Evento> CrearEvento(EventoCrearDTO eventoDto);
    }
}
