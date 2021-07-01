using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public ActionResult ObtenerClientes()
        {
            string[] clientes = { "Juan", "Pepe", "Ana", "Francisca" };

            if (!clientes.Any())
                return NotFound();
            return Ok(clientes);
        }

        [HttpPost]
        public ActionResult CrearCliente(string nombre)
        {
            // validar y guardar en BD
            bool ocurrioAlgoMalo = false;

            if (ocurrioAlgoMalo)
                return BadRequest();
            return Created("", nombre);
        }

        [HttpDelete]
        public ActionResult BorrarCliente()
        {
            // validar y guardar en BD
            bool ocurrioAlgoMalo = false;

            if (ocurrioAlgoMalo)
                return BadRequest();
            return NoContent();
        }

    }
}
