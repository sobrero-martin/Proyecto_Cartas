using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Server.Hubs;
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
        private readonly IHubContext<JuegoHub> hubContext;
        private readonly IPerfilUsuarioRepositorio perfilUsuarioRepositorio;

        public JuegoController(IPartidaRepositorio partidaRepositorio,
                               ITurnoRepositorio turnoRepositorio,
                               IEventoRepositorio eventoRepositorio,
                               IEstadoCartaRepositorio estadoCartaRepositorio,
                               IUsuarioPartidaRepositorio usuarioPartidaRepositorio,
                               IHubContext<JuegoHub> hubContext,
                               IPerfilUsuarioRepositorio perfilUsuarioRepositorio)
        {
            this.partidaRepositorio = partidaRepositorio;
            this.turnoRepositorio = turnoRepositorio;
            this.eventoRepositorio = eventoRepositorio;
            this.estadoCartaRepositorio = estadoCartaRepositorio;
            this.usuarioPartidaRepositorio = usuarioPartidaRepositorio;
            this.hubContext = hubContext;
            this.perfilUsuarioRepositorio = perfilUsuarioRepositorio;
        }

        /*
        [HttpPost("buscarPartida/{id:int}")]
        public async Task<ActionResult<BuscarPartidaRespuestaDTO>> BuscarPartida(int perfilUsuarioId)
        */

        [HttpPost("buscarPartida")]
        public async Task<ActionResult<BuscarPartidaRespuestaDTO>> BuscarPartida(BuscarPartidaSolicitudDTO dto)
        {
            var respuesta = new BuscarPartidaRespuestaDTO();
            int perfilUsuarioId = dto.PerfilUsuarioId;

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

                respuesta.Aceptado = false;
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

                PerfilUsuarioDTO? perfilRival = null;

                if (rival != null)
                { 
                    perfilRival = await perfilUsuarioRepositorio.GetPerfilById(rival.PerfilUsuarioID); 
                }

                respuesta.Aceptado = false;
                respuesta.Mensaje = "Partida encontrada....";
                respuesta.PartidaId = partida.Id;
                respuesta.PerfilUsuarioRivalId = rival?.PerfilUsuarioID;

                return Ok(respuesta);
            }
        }

        [HttpGet("estado/{perfilUsuarioId:int}")]
        public async Task<ActionResult<RevisarEstadoDTO>> GetEstadoPartida(int perfilUsuarioId)
        {
            var partida = await usuarioPartidaRepositorio.RevisarPartidaEncontrada(perfilUsuarioId);

            if (partida == null)
            {
                return BadRequest("No se ha encontrado una partida para este usuario.");
            }



            await hubContext.Clients.Group($"partida-{partida.PartidaId}")
                            .SendAsync("RecibirNotificacionPartida", $"¡Se encontró una partida! {partida.NombreRival} vs {partida.Nombre}");

            return Ok(partida);
        }


        [HttpPost("confirmarPartida")]
        public async Task<ActionResult> ConfirmarPartida(int perfilUsuarioId)
        {
            var exito = await usuarioPartidaRepositorio.ConfirmarPartida(perfilUsuarioId);
            if (!exito) return BadRequest("No se pudo confirmar la partida");

            return Ok("Partida confirmada correctamente.");
        }

        [HttpPost("cancelarPartida")]
        public async Task<ActionResult> CancelarPartida(int perfilUsuarioId)
        {
            var exito = await usuarioPartidaRepositorio.CancelarPartida(perfilUsuarioId);

            if (!exito)
            {
                return BadRequest("No estás jugando o buscando una partida.");
            }

            await hubContext.Clients.All.SendAsync("RecibirNotificacion", $"El usuario {perfilUsuarioId} canceló la partida");

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
