using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error-development")]
        [HttpGet]
        public IActionResult ErrorDevlopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
                return NotFound();

            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(detail: exceptionHandlerFeature.Error.StackTrace, title: exceptionHandlerFeature.Error.Message);
        }

        [Route("/error")]
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
