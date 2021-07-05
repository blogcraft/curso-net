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

        [HttpGet]
        public ActionResult ObtenerClientes([FromQuery] int limite)
        {
            Cliente[] clientes = {
                new() { Nombre = "Juan", Apellicos = "Martinez", Direccion = "Calle A Nº 1234", Telefono = "+56 9 2728 1232"},
                new() { Nombre = "Pepe", Apellicos = "Martinez", Direccion = "Calle B Nº 1243", Telefono = "+56 9 2827 1232"},
                new() { Nombre = "Ana", Apellicos = "Martinez", Direccion = "Calle C Nº 1324", Telefono = "+56 9 2728 3212"},
                new() { Nombre = "Francisca", Apellicos = "Martinez", Direccion = "Calle D Nº 1342", Telefono = "+56 9 2712 2832"}
            };

            if (!clientes.Any())
                return NotFound();
            return Ok(clientes.Take((int)limite));
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
