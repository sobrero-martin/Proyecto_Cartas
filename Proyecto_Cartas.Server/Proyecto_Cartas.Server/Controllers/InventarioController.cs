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
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioRepositorio repositorio;
        public InventarioController(IInventarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }


        [HttpPost("MazoInicial/{perfilUsuarioId:int}/{opcion:int}")]
        public async Task<ActionResult<MensajeRespuestaDTO>> MazoInicial(int perfilUsuarioId, int opcion)
        {
            bool exito = await repositorio.MazoInicial(perfilUsuarioId, opcion);
            if (!exito)
            {
                return BadRequest(new MensajeRespuestaDTO
                {
                    Mensaje = "Error al crear el mazo inicial."
                });
            }
            return Ok(new MensajeRespuestaDTO
            {
                Mensaje = "Mazo inicial creado exitosamente."
            });
        }

        [HttpGet("{perfilUsuarioId:int}")]
        public async Task<ActionResult<List<InventarioDTO>>> GetByPerfilUsuarioId(int perfilUsuarioId)
        {
            var inventario = await repositorio.GetByPerfilUsuarioId(perfilUsuarioId);
            return Ok(inventario);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await repositorio.GetFull();

            var result = entities.Select(i => new InventarioDTO
            {
                PerfilUsuarioId = i.PerfilUsuarioID,
                CartaId = i.CartaID,
                Tipo = i.Tipo
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InventarioDTO dto)
        {
            var inventario = new Inventario
            {
                PerfilUsuarioID = dto.PerfilUsuarioId,
                CartaID = dto.CartaId,
                Tipo = dto.Tipo
            };

            await repositorio.Post(inventario);

            return CreatedAtAction(nameof(GetAll), new { id = inventario.Id }, inventario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InventarioDTO dto)
        {
            var entity = await repositorio.GetById(id);
            if (entity == null) return NotFound();

            entity.PerfilUsuarioID = dto.PerfilUsuarioId;
            entity.CartaID = dto.CartaId;
            entity.Tipo = dto.Tipo;

            await repositorio.Post(entity);

            return NoContent();
        }

    }
}
