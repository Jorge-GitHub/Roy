using Microsoft.AspNetCore.Mvc;
using Roy.Logging.Domain.DTO;

namespace Roy.Mvc.UT.API;

[Route("api/[controller]")]
[ApiController]
public class RoyTestController : ControllerBase
{
    [HttpPost("SaveException")]
    public IActionResult SaveException([FromBody] ExceptionDTO exception)
    {
        // Run custom code here.
        return base.Ok();
    }

    [HttpPost("Log")]
    public IActionResult Log([FromBody] LogDTO log)
    {
        // Run custom code here.
        return base.Ok();
    }
}