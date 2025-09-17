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
    public class RankingController : ControllerBase
    {
        private readonly IRankingRepositorio repositorio;
        public RankingController(IRankingRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Ranking>>> GetRankings()
        {
            var rankings = await repositorio.GetAllAsync();
            if (rankings == null)
            {
                return NotFound("No se encontraron rankings(NULL).");
            }
            if (!rankings.Any())
            {
                return Ok("No existen rankings.");
            }
            return Ok(rankings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ranking>> GetRanking(int id)
        {
            var ranking = await repositorio.GetByIdAsync(id);
            if (ranking == null)
            {
                return NotFound($"No se encontró el ranking con ID {id}.");
            }
            return Ok(ranking);
        }

        [HttpPost]
        public async Task<ActionResult> AddRanking(RankingDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("El objeto RankingDTO es nulo.");
            }
            var ranking = new Ranking
            {
                Puntos = dto.Puntos,
                Tipo = dto.Tipo,
                Posicion = dto.Posicion
            };
            try
            {
                await repositorio.AddRango(ranking);
                return CreatedAtAction(nameof(GetRanking), new { id = ranking.Id }, ranking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar el ranking: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRanking(int id, RankingDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("El objeto RankingDTO es nulo.");
            }
            var existingRanking = await repositorio.GetByIdAsync(id);
            if (existingRanking == null)
            {
                return NotFound($"No se encontró el ranking con ID {id}.");
            }
            existingRanking.Puntos = dto.Puntos;
            existingRanking.Tipo = dto.Tipo;
            existingRanking.Posicion = dto.Posicion;
            try
            {
                await repositorio.UpdateRanking(existingRanking);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error de concurrencia al actualizar el ranking.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el ranking: {ex.Message}");
            }
        
        }
    }
}
