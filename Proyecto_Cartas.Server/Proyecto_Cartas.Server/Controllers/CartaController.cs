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
    [Route("api/Carta")]
    public class CartaController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ICartaRepositorio repositorio;

        public CartaController(AppDbContext context, ICartaRepositorio repositorio)
        {
            this.context = context;
            this.repositorio = repositorio;
        }

        [HttpGet] // api/Carta

        public async Task<ActionResult<List<CartaDTO?>>> GetCartas()
        {
            var lista = await repositorio.GetFull();
            if (lista == null)
            {
                return NotFound("No se encontraron cartas, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen cartas en este momento.");
            }

            return Ok(lista);
        }

        [HttpGet("ordenarpornombre")] // api/Carta/ordenarpornombre

        public async Task<ActionResult<List<CartaDTO?>>> GetCartasPorNombre()
        {
            var lista = await repositorio.ListaNombreCarta();
            if (lista == null)
            {
                return NotFound("No se encontraron cartas, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen cartas en este momento.");
            }
            return Ok(lista);
        }

        [HttpGet("buscarpornombre/{nombre}")] // api/Carta/buscarpornombre/{nombre}

        public async Task<ActionResult<CartaDTO?>> GetCartasPorNombre(string nombre)
        {
            var carta = await repositorio.NombreCarta(nombre);
            if (carta == null)
            {
                return NotFound("No se encontraron cartas con ese nombre, VERIFICAR.");
            }
            return Ok(carta);
        }

        [HttpGet("ordenarporATK")] // api/Carta/ordenarporATK
        public async Task<ActionResult<List<CartaNumeroDTO?>>> GetCartasPorATK()
        {
            var lista = await repositorio.ListaATKCarta();
            if (lista == null)
            {
                return NotFound("No se encontraron cartas, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen cartas en este momento.");
            }
            return Ok(lista);
        }

        [HttpGet("ordernarporVIDA")] // api/Carta/ordernarporVIDA
        public async Task<ActionResult<List<CartaNumeroDTO?>>> GetCartasPorVIDA()
        {
            var lista = await repositorio.ListaVIDACarta();
            if (lista == null)
            {
                return NotFound("No se encontraron cartas, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen cartas en este momento.");
            }
            return Ok(lista);
        }

        [HttpGet("ordenarporVELOCIDAD")] // api/Carta/ordenarporVELOCIDAD
        public async Task<ActionResult<List<CartaNumeroDTO?>>> GetCartasPorVELOCIDAD()
        {
            var lista = await repositorio.ListaVelocidadCarta();
            if (lista == null)
            {
                return NotFound("No se encontraron cartas, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen cartas en este momento.");
            }
            return Ok(lista);
        }

        [HttpGet("ordenarpornivel")] // api/Carta/ordenarpornivel
        public async Task<ActionResult<List<CartaNumeroDTO?>>> GetCartasPorNivel()
        {
            var lista = await repositorio.ListaNivelCarta();
            if (lista == null)
            {
                return NotFound("No se encontraron cartas, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen cartas en este momento.");
            }
            return Ok(lista);
        }

        [HttpGet("buscarpornivel/{nivel}")] // api/Carta/buscarpornivel/{nivel}
        public async Task<ActionResult<List<CartaDTO?>>> GetCartasPorNivel(int nivel)
        {
            var carta = await repositorio.NivelCarta(nivel);
            if (carta == null)
            {
                return NotFound("No se encontraron cartas con ese nivel, VERIFICAR.");
            }
            return Ok(carta);
        }

        [HttpGet("ordenarportipo")] // api/Carta/ordenarportipo
        public async Task<ActionResult<List<CartaPalabraDTO?>>> GetCartasPorTipo()
        {
            var lista = await repositorio.ListaTipoCarta();
            if (lista == null)
            {
                return NotFound("No se encontraron cartas, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen cartas en este momento.");
            }
            return Ok(lista);
        }

        [HttpGet("buscarportipo/{tipo}")] // api/Carta/buscarportipo/{tipo}
        public async Task<ActionResult<List<CartaDTO?>>> GetCartasPorTipo(string tipo)
        {
            var carta = await repositorio.TipoCarta(tipo);
            if (carta == null)
            {
                return NotFound("No se encontraron cartas con ese tipo, VERIFICAR.");
            }
            return Ok(carta);
        }

        [HttpGet("{id:int}")] // api/Carta/{id}
        public async Task<ActionResult<CartaDTO>> GetCarta(int id)
        {
            var carta = await repositorio.GetById(id);
            if (carta == null)
            {
                return NotFound("No se encontro la carta con ese Id, VERIFICAR.");
            }
            return Ok(carta);
        }

    }
}
