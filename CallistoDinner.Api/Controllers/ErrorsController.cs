using CallistoDinner.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CallistoDinner.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;


            if (exception is SllException)
                return Problem(detail: exception?.Message, statusCode: 400);

            return Problem(detail: exception?.Message);
        }
    }
}
