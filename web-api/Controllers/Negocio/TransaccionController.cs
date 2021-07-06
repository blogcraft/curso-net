using Microsoft.AspNetCore.Mvc;
using web_api.Services;

namespace web_api.Controllers.Negocio
{
    [Route("api/negocio/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;
        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }
    }
}
