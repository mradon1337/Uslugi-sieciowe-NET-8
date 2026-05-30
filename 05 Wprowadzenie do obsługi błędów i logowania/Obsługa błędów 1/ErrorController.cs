using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("/api/error")]
    [ApiExplorerSettings(IgnoreApi = true)]   // zeby Swagger tego nie pokazywal
    public IActionResult HandleError()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        if (exceptionFeature != null)
        {
            Console.WriteLine(exceptionFeature.Error);   // log bledu do konsoli
            return Problem(detail: exceptionFeature.Error.Message, title: "Wystapil blad");
        }
        return Problem();
    }
}
