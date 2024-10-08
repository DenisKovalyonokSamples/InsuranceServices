﻿using DK.PolicyService.API.Commands.Dtos;
using DK.PolicyService.API.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK.PolicyService.API.Commands;

public class CreateOfferCommand : IRequest<CreateOfferResult>
{
    public string ProductCode { get; set; }
    public DateTime PolicyFrom { get; set; }
    public DateTime PolicyTo { get; set; }
    public List<string> SelectedCovers { get; set; }
    public List<QuestionAnswer> Answers { get; set; }
}

public class CreateOfferByAgentCommand : CreateOfferCommand, IRequest<CreateOfferResult>
{
    public CreateOfferByAgentCommand(string agentLogin, CreateOfferCommand baseCmd)
    {
        AgentLogin = agentLogin;
        ProductCode = baseCmd.ProductCode;
        PolicyFrom = baseCmd.PolicyFrom;
        PolicyTo = baseCmd.PolicyTo;
        SelectedCovers = baseCmd.SelectedCovers;
        Answers = baseCmd.Answers;
    }

    public string AgentLogin { get; set; }
}
