using Microsoft.AspNetCore.Mvc;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilUsuarioController : ControllerBase
    {

        private readonly IPerfilUsuarioRepositorio repositorio;

        public PerfilUsuarioController(IPerfilUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<PerfilUsuarioDTO>> GetByName(string nombre)
        {
            var perfil = await repositorio.GetPerfilByName(nombre);
            if (perfil == null)
            {
                return NotFound($"No profile found with name {nombre}.");
            }
            return Ok(perfil);
        }

        [HttpPut("userEdit")]
        public async Task<ActionResult> EditProfile(PerfilUsuarioCreateDTO dto)
        {
            var perfilUsuario = await repositorio.GetById(dto.UsuarioID);

            if (perfilUsuario == null)
            {
                return NotFound($"No profile found with user ID {dto.UsuarioID}.");
            }

            perfilUsuario.Nombre = dto.Nombre;
            perfilUsuario.Bio = dto.Bio;    


            await repositorio.Put(dto.UsuarioID, perfilUsuario);
            return Ok($"Row with id {dto.UsuarioID} correctly updated");
        }

        [HttpGet]
        public async Task<ActionResult<List<PerfilUsuario>>> GetFull()
        {
            var list = await repositorio.GetFull();

            if (list == null)
            {
                return NotFound("No list found(NULL).");
            }

            if (list.Count == 0)
            {
                return NotFound("No existing records on list.");
            }

            return Ok(list);
        }

    /*
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PerfilUsuario>> GetById(int id)
        {
            var entity = await repositorio.GetById(id);
            if (entity == null)
            {
                return NotFound($"No row found with ID {id}.");
            }
            return Ok(entity);
        }
    */


        [HttpPost]
        public async Task<ActionResult<int>> Post(PerfilUsuarioCreateDTO dto)
        {
            try
            {
                var perfil = new PerfilUsuario
                {
                    UsuarioID = dto.UsuarioID,
                    Nombre = dto.Nombre,
                    Bio = dto.Bio,
                };

                int id = await repositorio.Post(perfil);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /*
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, PerfilUsuario perfilUsuario)
        {
            var result = await repositorio.Put(id, perfilUsuario);
            return Ok($"Row with id {id} correctly updated");
        }
        */

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await repositorio.Delete(id);
            if (!result)
            {
                return NotFound($"No row found with ID {id} to delete.");
            }
            return Ok($"Row with id {id} correctly deleted");
        }
    }
}
