using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public string[] ObtenerClientes()
        {
            string[] clientes = { "Juan", "Pepe", "Ana", "Francisca" };
            return clientes;
        }

        [HttpPost]
        public void CrearCliente() { }

        [HttpDelete]
        public void BorrarCliente() { }

    }
}
