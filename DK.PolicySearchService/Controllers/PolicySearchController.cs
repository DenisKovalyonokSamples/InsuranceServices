using DK.PolicySearchService.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DK.PolicySearchService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PolicySearchController : ControllerBase
{
    private readonly IMediator bus;

    public PolicySearchController(IMediator bus)
    {
        this.bus = bus;
    }


    // GET api/values
    [HttpGet]
    public async Task<ActionResult> SearchAsync([FromQuery] string q)
    {
        var result = await bus.Send(new FindPolicyQuery { QueryText = q });
        return new JsonResult(result);
    }
}
