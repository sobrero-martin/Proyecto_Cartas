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
    public class AmigoController : ControllerBase
    {
        private readonly IAmigoRepositorio repositorio;

        public AmigoController(IAmigoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Amigo>>> GetAmigos()
        {
            var amigos = await repositorio.GetAll();
            if (amigos == null)
            {
                return NotFound("No se encontraron amigos(NULL).");
            }
            if (!amigos.Any())
            {
                return Ok("No existen amigos.");
            }
            return Ok(amigos);
        }
        /*
        [HttpGet("{id}")]
        public async Task<ActionResult<Amigo>> GetAmigo(int id)
        {
            var amigo = await repositorio.GetPorId(id);
            if (amigo == null)
            {
                return NotFound($"No se encontró el amigo con ID {id}.");
            }
            return Ok(amigo);
        }
        */
        [HttpGet("usuario/{usuarioId:int}")]
        public async Task<ActionResult<List<Amigo>>> GetAmigosPorUsuario(int usuarioId)
        {
            var amigos = await repositorio.GetAmigosPorUsuario(usuarioId);
            if (amigos == null || !amigos.Any())
            {
                return NotFound($"No se encontraron amigos para el usuario con ID {usuarioId}.");
            }
            return Ok(amigos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AmigoDTO>> GetByName(int id)
        {
            var perfil = await repositorio.GetPerfilById(id);
            if (perfil == null)
            {
                return NotFound($"No profile found with name {id}.");
            }
            return Ok(perfil);
        }

            [HttpPost]
        public async Task<ActionResult> AddAmigo(AmigoDTO dto)
        {
            var amigo = new Amigo
            {
                UsuarioId2 = dto.UsuarioId2,
                Fecha = dto.Fecha,
                Estado = dto.Estado
            };

            await repositorio.AddAmigo(amigo);
            return CreatedAtAction(nameof(GetAmigos), new { id = amigo.Id }, amigo);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAmigo(int id)
        {
            var existingAmigo = await repositorio.GetPorId(id);
            if (existingAmigo == null)
            {
                return NotFound($"No se encontró el amigo con ID {id}.");
            }
            try
            {
                await repositorio.DeleteAmigo(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el amigo: {ex.Message}");
            }
        }

    }
}
