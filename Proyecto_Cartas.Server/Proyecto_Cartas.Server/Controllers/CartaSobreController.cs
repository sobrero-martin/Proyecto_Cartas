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
    [Route("api/CartaSobre")]
    public class CartaSobreController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ICartaSobreRepositorio repositorio;

        public CartaSobreController(AppDbContext context, ICartaSobreRepositorio repositorio)
        {
            this.context = context;
            this.repositorio = repositorio;
        }

        [HttpGet] // api/CartaSobre

        public async Task<ActionResult<List<CartaSobreDTO?>>> Get()
        {
            var lista = await repositorio.GetFull();
            if (lista == null)
            {
                return NotFound("No se encontraron datos, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen datos en este momento.");
            }

            return Ok(lista);
        }

        [HttpGet("listasobrenombre")] // api/CartaSobre/listasobrenombre

        public async Task<ActionResult<List<CartaSobreDTO?>>> GetCartasSobresLista()
        {
            var lista = await repositorio.ListaCartaSobre();
            if (lista == null)
            {
                return NotFound("No se encontraron datos, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen datos en este momento.");
            }
            return Ok(lista);
        }

        [HttpGet("buscarpornombresobre/{nombre}")] // api/CartaSobre/buscarpornombresobre/{nombre}

        public async Task<ActionResult<List<CartaSobreDTO?>>> GetCartaSobrePorSobreNombre(string nombre)
        {
            var lista = await repositorio.ListaCartaSobreNombre(nombre);
            if (lista == null)
            {
                return NotFound("No se encontraron sobres con ese nombre, VERIFICAR.");
            }
            return Ok(lista);
        }

        [HttpGet("buscarpornombrecarta/{nombre}")] // api/CartaSobre/buscarpornombrecarta/{nombre}

        public async Task<ActionResult<List<CartaSobreDTO?>>> GetCartaSobrePorCartaNombre(string nombre)
        {
            var lista = await repositorio.ListaCartaSobreNombreCarta(nombre);
            if (lista == null)
            {
                return NotFound("No se encontraron cartas con ese nombre, VERIFICAR.");
            }
            return Ok(lista);
        }

        [HttpGet("{id:int}")] // api/CartaSobre/{id}

        public async Task<ActionResult<CartaSobreDTO?>> Get(int id)
        {
            var cartasobre = await repositorio.GetById(id);
            if (cartasobre == null)
            {
                return NotFound($"No se encontró el dato con id {id}");
            }
            return Ok(cartasobre);
        }
    }
}
