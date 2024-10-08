namespace DK.PaymentService.Domain;

public class PolicyAccountNumberGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}
