using Microsoft.AspNetCore.Mvc;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class JuegoController : ControllerBase
    {
        private readonly IPartidaRepositorio partidaRepositorio;
        private readonly ITurnoRepositorio turnoRepositorio;
        private readonly IEventoRepositorio eventoRepositorio;
        private readonly IEstadoCartaRepositorio estadoCartaRepositorio;
        private readonly IUsuarioPartidaRepositorio usuarioPartidaRepositorio;

        public JuegoController(IPartidaRepositorio partidaRepositorio,
                               ITurnoRepositorio turnoRepositorio,
                               IEventoRepositorio eventoRepositorio,
                               IEstadoCartaRepositorio estadoCartaRepositorio,
                               IUsuarioPartidaRepositorio usuarioPartidaRepositorio)
        {
            this.partidaRepositorio = partidaRepositorio;
            this.turnoRepositorio = turnoRepositorio;
            this.eventoRepositorio = eventoRepositorio;
            this.estadoCartaRepositorio = estadoCartaRepositorio;
            this.usuarioPartidaRepositorio = usuarioPartidaRepositorio;
        }

        [HttpPost("buscarPartida")]
        public async Task<ActionResult<BuscarPartidaRespuestaDTO>> BuscarPartida(int perfilUsuarioId)
        {
            var respuesta = new BuscarPartidaRespuestaDTO();

            if (await usuarioPartidaRepositorio.JugadorYaEnPartida(perfilUsuarioId))
            {
                respuesta.Aceptado = false;
                respuesta.Mensaje = "Ya estás en una partida en progreso.";
                return BadRequest(respuesta);
            }

            if (await usuarioPartidaRepositorio.JugadorBuscandoPartida(perfilUsuarioId))
            {
                respuesta.Aceptado = false;
                respuesta.Mensaje = "Ya estás buscando partida.";
                return BadRequest(respuesta);
            }

            var partida = await partidaRepositorio.BuscarPartidaDisponible();

            if (partida == null)
            {
                partida = await partidaRepositorio.CrearPartida();

                await usuarioPartidaRepositorio.AgregarJugador(partida, perfilUsuarioId);

                respuesta.Aceptado = true;
                respuesta.Mensaje = "Partida creada. Esperando a otro jugador...";
                respuesta.PartidaId = partida.Id;
                respuesta.PerfilUsuarioRivalId = 0;
                return Ok(respuesta);
            }
            else
            {
                await usuarioPartidaRepositorio.AgregarJugador(partida, perfilUsuarioId);
                var jugadores = await usuarioPartidaRepositorio.ObtenerJugadoresEnPartida(partida.Id);
                var rival = jugadores.FirstOrDefault(j => j.PerfilUsuarioID != perfilUsuarioId);

                respuesta.Aceptado = true;
                respuesta.Mensaje = "Te has unido a una partida.";
                respuesta.PartidaId = partida.Id;
                respuesta.PerfilUsuarioRivalId = rival?.PerfilUsuarioID;
                return Ok(respuesta);
            }
        }

        [HttpPost("cancelarPartida")]
        public async Task<ActionResult> CancelarPartida(int perfilUsuarioId)
        {
            var exito = await usuarioPartidaRepositorio.CancelarPartida(perfilUsuarioId);

            if (!exito)
            {
                return BadRequest("No estás jugando o buscando una partida.");
            }

            return Ok("Partida cancelada correctamente.");
        }




        // ENDPOINTS DE TESTEO

        [HttpGet("partida")]
        public async Task<ActionResult<List<Partida>>> GetFullPartida()
        {
            var list = await partidaRepositorio.GetFull();

            if (list == null)
            {
                return NotFound("No list found(NULL).");
            }

            if (list.Count == 0)
            {
                return NotFound("No existing records on list.");
            }

            return Ok(list);
        }

        [HttpDelete("partida/{id:int}")]
        public async Task<ActionResult> DeletePartida(int id)
        {
            var result = await partidaRepositorio.Delete(id);
            if (!result)
            {
                return NotFound($"No row found with ID {id} to delete.");
            }
            return Ok($"Row with id {id} correctly deleted");
        }

        [HttpGet("usuarioPartida")]
        public async Task<ActionResult<List<UsuarioPartida>>> GetFullUsuarioPartida()
        {
            var list = await usuarioPartidaRepositorio.GetFull();

            if (list == null)
            {
                return NotFound("No list found(NULL).");
            }

            if (list.Count == 0)
            {
                return NotFound("No existing records on list.");
            }

            return Ok(list);
        }

        [HttpDelete("usuarioPartida/{id:int}")]
        public async Task<ActionResult> DeleteUsuarioPartida(int id)
        {
            var result = await usuarioPartidaRepositorio.Delete(id);
            if (!result)
            {
                return NotFound($"No row found with ID {id} to delete.");
            }
            return Ok($"Row with id {id} correctly deleted");
        }
    }
}
