using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Server.Controllers
{
    [ApiController]
    [Route("api/Sobre")]
    public class SobreController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ISobreRepositorio repositorio;

        public SobreController(AppDbContext context, ISobreRepositorio repositorio)
        {
            this.context = context;
            this.repositorio = repositorio;
        }

        [HttpGet] // api/Sobre

        public async Task<ActionResult<List<SobreDTO?>>> Get()
        {
            var lista = await repositorio.GetFull();
            if (lista == null)
            {
                return NotFound("No se encontraron sobres, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen sobres en este momento.");
            }

            return Ok(lista);
        }

        [HttpGet("ordenarpornombre")] // api/Sobre/ordenarpornombre

        public async Task<ActionResult<List<SobreDTO?>>> GetSobresPorNombre()
        {
            var lista = await repositorio.ListaNombreSobre();
            if (lista == null)
            {
                return NotFound("No se encontraron sobres, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen sobres en este momento.");
            }
            return Ok(lista);
        }

        [HttpGet("buscarpornombre/{nombre}")] // api/Sobre/buscarpornombre/{nombre}

        public async Task<ActionResult<SobreDTO?>> GetSobrePorNombre(string nombre)
        {
            var carta = await repositorio.NombreSobre(nombre);
            if (carta == null)
            {
                return NotFound("No se encontraron sobres con ese nombre, VERIFICAR.");
            }
            return Ok(carta);
        }

        [HttpGet("{id:int}")] // api/Sobre/{id}

        public async Task<ActionResult<SobreDTO?>> Get(int id)
        {
            var sobre = await repositorio.GetById(id);
            if (sobre == null)
            {
                return NotFound($"No se encontró el sobre con id {id}");
            }
            return Ok(sobre);
        }

    }
}
