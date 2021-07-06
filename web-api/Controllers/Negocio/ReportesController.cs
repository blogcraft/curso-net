using Microsoft.AspNetCore.Mvc;
using web_api.Services;

namespace web_api.Controllers.Negocio
{
    [Route("api/negocio/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ICarteraService _carteraService;
        private readonly IClienteService _clienteService;

        public ReportesController(ICarteraService carteraService, IClienteService clienteService)
        {
            _carteraService = carteraService;
            _clienteService = clienteService;
        }
