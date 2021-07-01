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
        public void CrearCliente() { }

        [HttpDelete]
        public void BorrarCliente() { }

    }
}
