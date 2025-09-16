using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Cartas.BD.Datos;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio repositorio;

        public UsuarioController(IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UsuarioAuthDTO login)
        {
            var res = await repositorio.Login(login);

            if (res == 0)
                return NotFound("Email doesn't exist or the password is incorrect");


            return Ok("Login was succesful");
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UsuarioAuthDTO dto)
        {
            var usuarioId = await repositorio.Register(dto);

            if (usuarioId == 0)
                return BadRequest("El email ya está registrado");

            return Ok("Usuario registrado correctamente");
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

        /*
        [HttpPost]
        public async Task<ActionResult<int>> PostUsuario(Usuario usuario)
        {
            try
            {
                await repositorio.Post(usuario);   
                //await context.Usuarios.AddAsync(usuario);
                return Ok(usuario.Id);
            }
            catch (Exception e) 
            { 
                return BadRequest(e.Message);
            }
           
        }
        */

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutUsuario(int id, Usuario usuario)
        {
            // if (id != usuario.Id)
            //{
            //  return BadRequest("Datos no válidos.");
            //}
            //var existe = await context.Usuarios.AnyAsync(x => x.Id == id);
            //var existe = await repositorio.Existe(id);

            //if (!existe)
            //{
            //    return NotFound($"No se encontró el usuario con id {id}");
            //}
            //context.Update(usuario);
            //await context.SaveChangesAsync();
            var resultado = await repositorio.Put(id, usuario);
            return Ok($"Usuario con id {id} actualizado correctamente");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            //var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            //if (usuario == null)
            //{
            //    return NotFound($"No se encontró el usuario con id {id}");
            //}
            //context.Usuarios.Remove(usuario);
            //await context.SaveChangesAsync();
            var resultado = await repositorio.Delete(id);
            if (!resultado) return NotFound($"No se encontró el usuario con id {id}");

            return Ok($"Usuario con id {id} eliminado correctamente");
        }


    }
}
