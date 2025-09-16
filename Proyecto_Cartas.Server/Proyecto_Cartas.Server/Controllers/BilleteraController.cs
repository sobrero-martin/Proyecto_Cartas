using Microsoft.AspNetCore.Mvc;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;

namespace Proyecto_Cartas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BilleteraController : ControllerBase
    {

        private readonly IBilleteraRepositorio repositorio;

        public BilleteraController(IBilleteraRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Billetera>>> GetFull()
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Billetera>> GetById(int id)
        {
            var entity = await repositorio.GetById(id);
            if (entity == null)
            {
                return NotFound($"No row found with ID {id}.");
            }
            return Ok(entity);
        }



        [HttpPost]
        public async Task<ActionResult<int>> Post(Billetera billetera)
        {
            try
            {
                await repositorio.Post(billetera);
                return Ok(billetera.Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Billetera billetera)
        {
            var result = await repositorio.Put(id, billetera);
            return Ok($"Row with id {id} correctly updated");
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
