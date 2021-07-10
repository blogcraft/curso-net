using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_api.Services;

namespace web_api.Controllers.Sistema
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("api/sistema/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrador")]
        [HttpPost("SignUpAdministrador")]
        public async Task<IActionResult> SignUpAdministrador([FromBody] SignUpParameters param)
        {
            IEnumerable<IdentityError> errors = await _accountService.CrearAdminAsync(param);
            if (errors == null)
            {
                return Ok();
            }
            else
            {
                return BadRequest(errors);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("SignUpVisita")]
        public async Task<IActionResult> SignUpVisita([FromBody] SignUpParameters param)
        {
            IEnumerable<IdentityError> errors = await _accountService.CrearVisitAsync(param);
            if (errors == null)
            {
                return Ok();
            }
            else
            {
                return BadRequest(errors);
            }
        }
    }
}
