using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace apiSupplier.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/error")]
        private IActionResult Error([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception innerExcepcion = context.Error;

            while (innerExcepcion.InnerException != null) innerExcepcion = innerExcepcion.InnerException;
            _logger.LogError("Error Path: {0}, Source:{1}, Message:{2}, InnerException:{3}, Trace:{4} ", "", context.Error.Message, context.Error.StackTrace, context.Error.Source, innerExcepcion.Message);

            if (webHostEnvironment.EnvironmentName != "Development")
            {
                return Problem();
            }
            else
            {
                return Problem(
                    detail: innerExcepcion.Message,
                    title: context.Error.Message);
            }
        }
    }
}
