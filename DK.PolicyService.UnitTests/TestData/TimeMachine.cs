using DK.PolicyService.Domain;

namespace DK.PolicyService.UnitTests.TestData;

public class TimeMachine : IDisposable
{
    private readonly Func<DateTime> oldTimeProvider;

    public TimeMachine(DateTime newNow)
    {
        oldTimeProvider = SysTime.CurrentTimeProvider;
        SysTime.CurrentTimeProvider = () => newNow;
    }

    public void Dispose()
    {
        SysTime.CurrentTimeProvider = oldTimeProvider;
    }
}