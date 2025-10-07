using Microsoft.AspNetCore.Mvc;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Server.Controllers
{
    [ApiController]
    [Route("api/CartaApertura")]
    public class CartaAperturaController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ICartaAperturaRepositorio repositorio;

        public CartaAperturaController(AppDbContext context, ICartaAperturaRepositorio repositorio)
        {
            this.context = context;
            this.repositorio = repositorio;
        }

        [HttpGet] // api/CartaApertura
        public async Task<ActionResult<List<CartaAperturaMostrarDTO?>>> Get()
        {
            var lista = await repositorio.GetListaApertura();
            if (lista == null)
            {
                return NotFound("No se encontraron aperturas.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen aperturas.");
            }
            return Ok(lista);
        }

        [HttpGet("{id:int}")] // api/CartaApertura/{id}
        public async Task<ActionResult<CartaAperturaMostrarDTO?>> Get(int id)
        {
            var entidad = await repositorio.GetAperturaId(id);
            if (entidad == null)
            {
                return NotFound("No se encontro la apertura, VERIFICAR.");
            }
            return Ok(entidad);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CartaAperturaCrearDTO dto)
        {
            try
            {
                var apertura = new CartaApertura
                {
                    CompraSobreID = dto.CompraSobreID,
                    CartaSobreID = dto.CartaSobreID,
                    CantidadCartasObtenidas = dto.CantidadCartasObtenidas

                };
                int id = await repositorio.Post(apertura);

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
