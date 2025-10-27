﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<MensajeRespuestaDTO>> ConfirmarPartida(BuscarPartidaSolicitudDTO perfilUsuarioId)
        {
            int id = perfilUsuarioId.PerfilUsuarioId;
            var exito = await usuarioPartidaRepositorio.ConfirmarPartida(id);
            if (!exito) return BadRequest(new MensajeRespuestaDTO { Mensaje = "No se pudo confirmar la partida" });

            return Ok(new MensajeRespuestaDTO { Mensaje = "Partida confirmada correctamente." });
        }

        [HttpGet("obtenerPartidaPorJugador/{perfilUsuarioId:int}")]
        public async Task<ActionResult<int>> ObtenerPartidaPorJugador(int perfilUsuarioId)
        {
            int? partidaId = await usuarioPartidaRepositorio.BuscarPartidaPorJugador(perfilUsuarioId);

            if (partidaId == null)
            {
                return NotFound("El jugador no está en ninguna partida.");
            }

            return Ok(partidaId);
        }

        [HttpGet("buscarUsuarioPartida/{perfilUsuarioId:int}")]
        public async Task<ActionResult<int>> BuscarUsuarioPartida(int perfilUsuarioId)
        {
            int? usuarioPartidaId = await usuarioPartidaRepositorio.BuscarUsuarioPartida(perfilUsuarioId);

            if (usuarioPartidaId == null)
            {
                return NotFound("El jugador no está en ninguna partida.");
            }

            return Ok(usuarioPartidaId);
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

        #region EstadoCarta

        [HttpGet("estadoCarta")]
        public async Task<ActionResult<List<EstadoCartaDTO>>> Get()
        {
            var list = await estadoCartaRepositorio.GetFull();

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


        [HttpGet("estadoCarta/{id:int}")] // api/estadoCarta/{id}

        public async Task<ActionResult<EstadoCartaDTO>> Get(int id)
        {
            var estado = await estadoCartaRepositorio.GetById(id);
            if (estado == null)
            {
                return NotFound($"No se encontró el dato con id {id}");
            }
            return Ok(estado);
        }

        [HttpGet("estadoCarta/usuario/{usuarioPartidaId:int}")] // api/estadoCarta/usuario/{usuarioPartidaId}

        public async Task<ActionResult<List<EstadoCartaDTO>>> GetEstadoCartaDeUnUsuario(int usuarioPartidaId)
        {
            var estado = await estadoCartaRepositorio.EstadoCartaDeUnUsuario(usuarioPartidaId);
            if (estado == null)
            {
                return NotFound($"No se encontró el dato con id {usuarioPartidaId}");
            }
            if (estado.Count == 0)
            {
                return Ok("No existen datos en este momento.");
            }
            return Ok(estado);
        }

        [HttpGet("estadoCarta/filtrarPosicion/{usuarioPartidaId:int}/{posicion}")] // api/estadoCarta/filtrarPosicion/{usuarioPartidaId}/{posicion}

        public async Task<ActionResult<List<EstadoCartaDTO>>> GetFiltrarPosicion(int usuarioPartidaId, string posicion)
        {
            var estado = await estadoCartaRepositorio.FiltrarPosicion(usuarioPartidaId, posicion);
            if (estado == null)
            {
                return NotFound($"No se encontró el dato con id {usuarioPartidaId} y posición {posicion}");
            }
            if (estado.Count == 0)
            {
                return Ok("No existen datos en este momento.");
            }
            return Ok(estado);
        }

        [HttpGet("estadoCarta/CartasEnCampo/{usuarioPartidaId:int}")] // api/estadoCarta/CartasEnCampo/{usuarioPartidaId})]
        public async Task<ActionResult<List<EstadoCartaDTO>>> GetCartasEnCampo(int usuarioPartidaId)
        {
            var estado = await estadoCartaRepositorio.CartasEnCampo(usuarioPartidaId);
            if (estado == null)
            {
                return NotFound($"No se encontró el dato con id {usuarioPartidaId}");
            }
            if (estado.Count == 0)
            {
                return Ok("No existen datos en este momento.");
            }
            return Ok(estado);
        }

        [HttpPut("estadoCarta/robarCarta/{usuarioPartidaId:int}/{turnoId:int}")] // api/estadoCarta/robarCarta/{usuarioPartidaId}/{turnoId}

        public async Task<ActionResult<EstadoCartaDTO>> PutRobarCarta(int usuarioPartidaId, int turnoId)
        {
            var estado = await estadoCartaRepositorio.RobarCarta(usuarioPartidaId, turnoId);
            if (estado == null)
            {
                return NotFound($"No se pudo robar carta para el Usuario: {usuarioPartidaId}");
            }
            return Ok(estado);
        }

        [HttpPut("estadoCarta/colocarEnCampo/{usuarioPartidaId:int}/{cartaId:int}/{lugar:int}/{turnoId:int}")] // api/estadoCarta/colocarEnCampo/{usuarioPartidaId}/{cartaId}/{lugar}/{turnoId}

        public async Task<ActionResult<EstadoCartaDTO>> PutColocarEnCampo(int usuarioPartidaId, int cartaId, int lugar, int turnoId)
        {
            var estado = await estadoCartaRepositorio.ColocarEnCampo(usuarioPartidaId, cartaId, lugar, turnoId);
            if (estado == null)
            {
                return BadRequest($"No se pudo colocar en campo la carta en el Campo{lugar}");
            }
            return Ok(estado);
        }

        [HttpPut("estadoCarta/enviarAlCementerio/{usuarioPartidaId:int}/{cartaId:int}/{turnoId:int}")] // api/estadoCarta/enviarAlCementerio/{usuarioPartidaId}/{cartaId}/{turnoId}

        public async Task<ActionResult<EstadoCartaDTO>> PutEnviarAlCementerio(int usuarioPartidaId, int cartaId, int turnoId)
        {
            var estado = await estadoCartaRepositorio.EnviarAlCementerio(usuarioPartidaId, cartaId, turnoId);
            if (estado == null)
            {
                return BadRequest($"No se pudo enviar al cementerio la carta {cartaId}");
            }
            return Ok(estado);
        }

        [HttpPut("estadoCarta/robarCartasCantidad/{usuarioPartidaId:int}/{cantidad:int}/{turnoId:int}")] // api/estadoCarta/robarCartasCantidad/{usuarioPartidaId}/{cantidad}/{turnoId}

        public async Task<ActionResult<List<EstadoCartaDTO>>> PutRobarCartasCantidad(int usuarioPartidaId, int cantidad, int turnoId)
        {
            var estado = await estadoCartaRepositorio.RobarCartasCantidad(usuarioPartidaId, cantidad, turnoId);
            if (estado == null)
            {
                return NotFound($"No se pudieron robar {cantidad} cartas para el Usuario: {usuarioPartidaId}");
            }
            if (estado.Count == 0)
            {
                return Ok("No existen datos en este momento.");
            }
            return Ok(estado);
        }


        #endregion

        [HttpPost("turno")] // api/turno

        public async Task<ActionResult<TurnoDTO>> NuevoTurno(TurnoCrearDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("El turno no puede ser nulo.");
            }
            try
            {
                var turnoCreado = await turnoRepositorio.CrearTurno(dto);
                return CreatedAtAction(nameof(GetTurnoPorId), new { id = turnoCreado.Id }, turnoCreado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el turno: {ex.Message}");
            }

        }

        [HttpGet("turno/{id:int}")] // api/turno/{id}
        public async Task<ActionResult<TurnoDTO>> GetTurnoPorId(int id)
        {
            var turno = await turnoRepositorio.GetById(id);
            if (turno == null)
            {
                return NotFound($"No se encontró el turno con ID {id}.");
            }

            return Ok(turno);
        }
    }
}
