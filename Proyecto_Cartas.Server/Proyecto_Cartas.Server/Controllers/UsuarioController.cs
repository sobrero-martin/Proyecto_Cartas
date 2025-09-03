using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;

namespace Proyecto_Cartas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IRepositorio<Usuario> repositorio;

        public UsuarioController(AppDbContext context, IRepositorio<Usuario> repositorio)
        {
            this.context = context;
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>>  GetUsuarios()
        {
            //var usuarios = await context.Usuarios.ToListAsync();

            var usuarios = await repositorio.GetFull();

            if (usuarios == null)
            {
                return NotFound("No se encontraron usuarios(NULL).");
            }

            if (usuarios.Count == 0)
            {
                return Ok("No existen usuarios.");
            }

            return Ok(usuarios);
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
        public async Task<ActionResult<int>> PostUsuario(Usuario usuario)
        {
            try
            {
                await context.Usuarios.AddAsync(usuario);
                await context.SaveChangesAsync();
                return Ok(usuario.Id);
            }
            catch (Exception e) 
            { 
                return BadRequest(e.Message);
            }
           
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest("Datos no válidos.");
            }
            var existe = await context.Usuarios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"No se encontró el usuario con id {id}");
            }
            context.Update(usuario);
            await context.SaveChangesAsync();
            return Ok($"Usuario con id {id} actualizado correctamente");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario == null)
            {
                return NotFound($"No se encontró el usuario con id {id}");
            }
            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();
            return Ok($"Usuario con id {id} eliminado correctamente");
        }


    }
}
