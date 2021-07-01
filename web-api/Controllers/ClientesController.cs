using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public ActionResult ObtenerClientes([FromQuery] int limite)
        {
            string[] clientes = { "Juan", "Pepe", "Ana", "Francisca" };

            if (!clientes.Any())
                return NotFound();
            return Ok(clientes.Take((int)limite));
        }

        [HttpPost]
        public ActionResult CrearCliente([FromBody] string nombre)
        {
            // validar y guardar en BD
            bool ocurrioAlgoMalo = false;

            if (ocurrioAlgoMalo)
                return BadRequest();
            return Created("", nombre);
        }

        [HttpDelete("{id}")]
        public ActionResult BorrarCliente(string id)
        {
            // validar y guardar en BD
            bool ocurrioAlgoMalo = false;

            if (ocurrioAlgoMalo)
                return BadRequest();
            return NoContent();
        }

    }
}
