using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.Data.AppDb.Context;
using web_api.Data.AppDb.Model;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppdbContext _appdbContext;
        public ClientesController(AppdbContext appdbContext)
        {
            _appdbContext = appdbContext;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult> ObtenerClientes([FromQuery] int limite)
        {
            List<Cliente> clientes = await _appdbContext.Cliente.ToListAsync();

            if (!clientes.Any())
                return NotFound();
            return Ok(clientes.Take((int)limite));
        }

        // GET: api/Clientes/1
        [HttpGet("{id}")]
        public async Task<ActionResult> ObtenerCliente(int id)
        {
            Cliente cliente = await _appdbContext.Cliente.FindAsync(id);

            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult CrearCliente([FromBody] Cliente nombre)
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
