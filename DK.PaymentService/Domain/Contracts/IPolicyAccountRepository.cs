namespace DK.PaymentService.Domain.Contracts;

public interface IPolicyAccountRepository
{
    void Add(PolicyAccount policyAccount);

    void Update(PolicyAccount policyAccount);

    Task<PolicyAccount> FindByNumber(string accountNumber);
    Task<bool> ExistsWithPolicyNumber(string policyNumber);
}
