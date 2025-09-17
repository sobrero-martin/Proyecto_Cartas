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
                UsuarioId = up.UsuarioID,
                Aceptado = up.Aceptado
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioPartidaDTO dto)
        {
            var usuarioPartida = new UsuarioPartida
            {
                UsuarioID = dto.UsuarioId,
                Aceptado = dto.Aceptado
            };

            await repositorio.Post(usuarioPartida);

            return CreatedAtAction(nameof(GetAll), new { id = usuarioPartida.Id }, usuarioPartida);
        }
    }
}
