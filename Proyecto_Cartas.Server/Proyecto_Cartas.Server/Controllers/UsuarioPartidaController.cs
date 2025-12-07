using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioPartidaController : ControllerBase
    {
        private readonly IUsuarioPartidaRepositorio repositorio;
        public UsuarioPartidaController(IUsuarioPartidaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await repositorio.GetFull();

            var result = entities.Select(up => new UsuarioPartidaDTO
            {
                PerfilUsuarioId = up.PerfilUsuarioID,
                Aceptado = up.Aceptado
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioPartidaDTO dto)
        {
            var usuarioPartida = new UsuarioPartida
            {
                PerfilUsuarioID = dto.PerfilUsuarioId,
                Aceptado = dto.Aceptado
            };

            await repositorio.Post(usuarioPartida);

            return CreatedAtAction(nameof(GetAll), new { id = usuarioPartida.Id }, usuarioPartida);
        }

        [HttpGet("nombre/{usuarioPartidaId:int}")]
        public async Task<ActionResult<MensajeRespuestaDTO>> GetNombre(int usuarioPartidaId)
        {
            var nombre = await repositorio.NombreJugador(usuarioPartidaId);

            if(nombre == "")
            {
                return NotFound(new MensajeRespuestaDTO { Mensaje = "UsuarioPartida no encontrado." });
            }

            return Ok(new MensajeRespuestaDTO { Mensaje = nombre });
        }
    }
}
