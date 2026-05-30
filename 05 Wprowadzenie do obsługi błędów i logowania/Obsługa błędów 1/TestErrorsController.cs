using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TestErrorsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        throw new Exception("Testowy wyjatek");
    }
}
