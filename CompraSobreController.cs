using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Server.Controllers
{
    [ApiController]
    [Route("api/CompraSobre")]
    public class CompraSobreController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ICompraSobreRepositorio repositorio;

        public CompraSobreController(AppDbContext context, ICompraSobreRepositorio repositorio)
        {
            this.context = context;
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompraSobreMostrarDTO?>>>  Get()
        {

            var lista = await repositorio.GetFull();

            if (lista == null)
            {
                return NotFound("No se encontraron compras.");
            }

            if (lista.Count == 0)
            {
                return Ok("No existen compras.");
            }

            return Ok(lista);
        }

        [HttpGet("lista")] // api/CompraSobre/lista
        public async Task<ActionResult<List<CompraSobreMostrarDTO?>>> GetLista()
        {

            var lista = await repositorio.ListaCompraSobre();

            if (lista == null)
            {
                return NotFound("No se encontraron compras.");
            }

            if (lista.Count == 0)
            {
                return Ok("No existen compras.");
            }

            return Ok(lista);
        }


        [HttpGet("fechacompra/{fecha}")] // api/CompraSobre/fechacompra/2023-01-01

        public async Task<ActionResult<List<CompraSobreMostrarDTO?>>> GetFechaDeCompra(DateTime fecha)
        {
            var lista = await repositorio.FechaDeCompra(fecha);
            if (lista == null)
            {
                return NotFound("No se encontraron compras.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen compras en esa fecha.");
            }
            return Ok(lista);
        }

        [HttpGet("usuario/{nombreUsuario}")] // api/CompraSobre/usuario/nombreUsuario

        public async Task<ActionResult<List<CompraSobreMostrarDTO?>>> GetListaUsuario(string nombreUsuario)
        {
            var lista = await repositorio.ListaCompraSobreUsuario(nombreUsuario);
            if (lista == null)
            {
                return NotFound("No se encontraron compras.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen compras para ese usuario.");
            }
            return Ok(lista);
        }

        [HttpGet("sobre/{nombreSobre}")] // api/CompraSobre/sobre/nombreSobre
        public async Task<ActionResult<List<CompraSobreMostrarDTO?>>> GetListaQuienComproSobre(string nombreSobre)
        {
            var lista = await repositorio.ListaQuienComproSobre(nombreSobre);
            if (lista == null)
            {
                return NotFound("No se encontraron compras.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen compras para ese sobre.");
            }
            return Ok(lista);
        }

        [HttpGet("{id:int}")] 
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            //var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            var usuario = await repositorio.GetById(id);
            if (usuario == null)
            {
                return NotFound($"No se encontró el usuario con id {id}");
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CompraSobreCrearDTO dto)
        {
            try
            {
                var compra =new CompraSobre
                {
                    UsuarioID = dto.UsuarioID,
                    SobreID = dto.SobreID,
                    FechaCompra = dto.FechaCompra
                };
                int id = await repositorio.Post(compra);   
                
                return Ok(id);
            }
            catch (Exception e) 
            { 
                return BadRequest(e.Message);
            }
           
        }

    }
}
