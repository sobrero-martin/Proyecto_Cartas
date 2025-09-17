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
    public class PartidaController : ControllerBase
    {
        private readonly IPartidaRepositorio repositorio;
        public PartidaController(IPartidaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await repositorio.GetFull();

            var result = entities.Select(p => new PartidaDTO
            {
                Estado = p.Estado,
                Ganador = p.Ganador
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartidaDTO dto)
        {
            var partida = new Partida
            {
                Estado = dto.Estado,
                Ganador = dto.Ganador
            };

            await repositorio.Post(partida);

            return CreatedAtAction(nameof(GetAll), new { id = partida.Id }, partida);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, PartidaDTO dto)
        {
            var entity = await repositorio.GetById(id);
            if (entity == null) return NotFound();

            entity.Estado = dto.Estado;
            entity.Ganador = dto.Ganador;

            await repositorio.Post(entity);

            return NoContent();
        }
    }
}
