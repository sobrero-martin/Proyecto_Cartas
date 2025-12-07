using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Cartas.Repositorio.Repositorios
{
    public interface ITurnoRepositorio : IRepositorio<Turno>
    {
        
        Task<TurnoDTO> CrearTurno(TurnoCrearDTO turnoDTO);
        Task<bool> ExisteRoboInicial(int usuarioPartidaId);
        Task<bool> PresionoTerminarTurno(int usuarioPartidaId, int numero, string fase);
        Task<TurnoDTO?> UltimoTurno(int usuarioPartidaId);
    }
}
