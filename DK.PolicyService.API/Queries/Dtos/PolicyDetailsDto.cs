﻿namespace DK.PolicyService.API.Queries.Dtos;

public class PolicyDetailsDto
{
    public string Number { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string PolicyHolder { get; set; }
    public decimal TotalPremium { get; set; }
    public string ProductCode { get; set; }
    public string AccountNumber { get; set; }

    public List<string> Covers { get; set; }
}
