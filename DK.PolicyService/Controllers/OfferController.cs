using DK.PolicyService.API.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static System.String;

namespace DK.PolicyService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OfferController : ControllerBase
{
    private readonly IMediator bus;

    public OfferController(IMediator bus)
    {
        this.bus = bus;
    }

    // POST api/values
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateOfferCommand cmd, [FromHeader] string AgentLogin)
    {
        var result = IsNullOrWhiteSpace(AgentLogin)
            ? await bus.Send(cmd)
            : await bus.Send(new CreateOfferByAgentCommand(AgentLogin, cmd));
        return new JsonResult(result);
    }
}
