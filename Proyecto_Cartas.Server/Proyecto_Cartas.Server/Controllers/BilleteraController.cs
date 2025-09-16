using Microsoft.AspNetCore.Mvc;
using Proyecto_Cartas.BD.Datos.Entidades;
using Proyecto_Cartas.Repositorio.Repositorios;
using Proyecto_Cartas.Shared.DTO;

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


        [HttpGet("{userId:int}")]
        public async Task<ActionResult<BilleteraDTO>> GetByUsuarioId(int userId)
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
        public async Task<ActionResult<Billetera>> GetById(int id)
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
        public async Task<ActionResult<int>> Post(BilleteraDTO billetera)
        {
            try
            {
                var nuevaBilletera = new Billetera
                {
                    UsuarioID = billetera.UsuarioID,
                    cantidadMonedas = billetera.cantidadMonedas
                };
                int id = await repositorio.Post(nuevaBilletera);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(BilleteraDTO billetera)
        {
            var billeteraExistente = await repositorio.GetById(billetera.UsuarioID);
            if (billeteraExistente == null)
            {
                return NotFound($"No row found with ID {billetera.UsuarioID} to update.");
            }
            var result = await repositorio.Put(billetera.UsuarioID, billeteraExistente);
            return Ok($"Row with id {billetera.UsuarioID} correctly updated");
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
