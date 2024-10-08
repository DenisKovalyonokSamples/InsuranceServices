﻿using DK.PaymentService.Domain.Contracts;
using DK.PaymentService.Domain;

namespace DK.PaymentService.UnitTests.Domain;

public class MockPolicyAccountRepository : IPolicyAccountRepository
{
    private readonly Dictionary<string, PolicyAccount> list = new();

    public MockPolicyAccountRepository()
    {
        list.Add("PA1", new PolicyAccount("POLICY_1", "231232132131", "Ann", "Smith"));
        list.Add("PA2", new PolicyAccount("POLICY_2", "389hfswjfrh2032r", "Jimmy", "Morrison"));
        list.Add("PA3", new PolicyAccount("POLICY_3", "0rju130fhj20", "Patrick", "Jones"));
    }

    public void Add(PolicyAccount policyAccount)
    {
        list.Add(policyAccount.PolicyNumber, policyAccount);
    }

    public void Update(PolicyAccount policyAccount)
    {
    }

    public Task<PolicyAccount> FindByNumber(string accountNumber)
    {
        return Task.FromResult(list.Values.FirstOrDefault(x => x.PolicyAccountNumber == accountNumber));
    }

    public Task<bool> ExistsWithPolicyNumber(string policyNumber)
    {
        return Task.FromResult(list.Values.Any(x => x.PolicyAccountNumber == policyNumber));
    }

    public ICollection<PolicyAccount> FindAll()
    {
        return list.Values;
    }

    public PolicyAccount FindForPolicy(string policyNumber)
    {
        return list.GetValueOrDefault(policyNumber);
    }
}
