using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api.Services;

namespace web_api.Controllers.Negocio
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("api/negocio/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;

        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        // POST: /api/negocio/Transaccion/Comprar
        [HttpPost("Comprar")]
        public async Task<IActionResult> Comprar([FromBody] TransaccionReq body)
        {
            await _transaccionService.ComprarAsync(body.ProductoId, body.CuentaId, body.Cantidad);

            return Ok();
        }

        // POST: /api/negocio/Transaccion/Vender
        [HttpPost("Vender")]
        public async Task<IActionResult> Vender([FromBody] TransaccionReq body)
        {
            await _transaccionService.VenderAsync(body.ProductoId, body.CuentaId, body.Cantidad);

            return Ok();
        }

        public class TransaccionReq
        {
            [Range(1, int.MaxValue)]
            public int ProductoId { get; set; }
            [Range(1, int.MaxValue)]
            public int CuentaId { get; set; }
            [Range(1, int.MaxValue)]
            public int Cantidad { get; set; }
        }
    }
}
