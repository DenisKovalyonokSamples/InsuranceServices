using DK.PolicyService.API.Commands;
using DK.PolicyService.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DK.PolicyService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PolicyController : ControllerBase
{
    private readonly IMediator bus;

    public PolicyController(IMediator bus)
    {
        this.bus = bus;
    }

    // POST 
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreatePolicyCommand cmd)
    {
        var result = await bus.Send(cmd);
        return new JsonResult(result);
    }

    // GET 
    [HttpGet("{policyNumber}")]
    public async Task<ActionResult> Get(string policyNumber)
    {
        var result = await bus.Send(new GetPolicyDetailsQuery { PolicyNumber = policyNumber });
        return new JsonResult(result);
    }

    // DELETE
    [HttpDelete("/terminate")]
    public async Task<ActionResult> Post([FromBody] TerminatePolicyCommand cmd)
    {
        var result = await bus.Send(cmd);
        return new JsonResult(result);
    }
}
