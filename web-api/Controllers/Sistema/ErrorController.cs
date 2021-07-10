using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers.Sistema
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                instance: context.Error.Source,
                title: context.Error.Message);
        }

        [Route("/error-debug")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ErrorDebug()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                instance: context.Error.Source,
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

    }
}
