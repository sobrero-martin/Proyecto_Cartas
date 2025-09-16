using Microsoft.AspNetCore.Mvc;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Shared.DTO;

namespace Proyecto_Cartas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfiguracionUsuarioController : ControllerBase
    {

        private readonly IConfiguracionUsuarioRepositorio repositorio;

        public ConfiguracionUsuarioController(IConfiguracionUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<ConfiguracionUsuario>>> GetFull()
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


        [HttpGet("{userId:int}")]
        public async Task<ActionResult<ConfiguracionUsuario>> GetById(int userId)
        {
            var entity = await repositorio.GetByUsuarioId(userId);
            if (entity == null)
            {
                return NotFound($"No row found with ID {userId}.");
            }
            return Ok(entity);
        }

        /*
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ConfiguracionUsuario>> GetById(int id)
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
        public async Task<ActionResult<int>> Post(ConfiguracionUsuarioDTO configUsuario)
        {
            try
            {
                var config = new ConfiguracionUsuario
                {
                    UsuarioID = configUsuario.UsuarioID,
                    VolumenMusica = configUsuario.VolumenMusica,
                    VolumenSFX = configUsuario.VolumenSFX,
                };
                await repositorio.Post(config);
                return Ok(config.Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(ConfiguracionUsuarioDTO configUsuario)
        {
            var configExistente = await repositorio.GetById(configUsuario.UsuarioID);
            if (configExistente == null)
            {
                return NotFound($"No row found with ID {configUsuario.UsuarioID} to update.");
            }
            var result = await repositorio.Put(configUsuario.UsuarioID, configExistente);
            return Ok($"Row with id {configUsuario.UsuarioID} correctly updated");
        }

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
