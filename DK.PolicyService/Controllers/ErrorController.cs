using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DK.PolicyService.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
        return exception switch
        {
            ApplicationException appEx => Problem(title: "Business rules violation", detail: exception.Message),
            _ => Problem()
        };
    }
}
