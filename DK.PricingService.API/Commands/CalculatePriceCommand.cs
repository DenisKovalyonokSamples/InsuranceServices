﻿using DK.PricingService.API.Commands.Dtos;
using DK.PricingService.API.Commands.Results;
using FluentValidation;
using MediatR;

namespace DK.PricingService.API.Commands;

public class CalculatePriceCommand : IRequest<CalculatePriceResult>
{
    public string ProductCode { get; set; }
    public DateTimeOffset PolicyFrom { get; set; }
    public DateTimeOffset PolicyTo { get; set; }
    public List<string> SelectedCovers { get; set; }
    public List<QuestionAnswer> Answers { get; set; }
}

public class CalculatePriceCommandValidator : AbstractValidator<CalculatePriceCommand>
{
    public CalculatePriceCommandValidator()
    {
        RuleFor(m => m.ProductCode).NotEmpty();
        RuleFor(m => m.SelectedCovers).NotNull();
        RuleFor(m => m.Answers).NotNull();
    }
}
