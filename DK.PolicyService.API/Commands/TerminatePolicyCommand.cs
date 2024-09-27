using DK.PolicyService.API.Commands.Results;
using MediatR;

namespace DK.PolicyService.API.Commands;

public class TerminatePolicyCommand : IRequest<TerminatePolicyResult>
{
    public string PolicyNumber { get; set; }
    public DateTime TerminationDate { get; set; }
}
