namespace DK.PolicyService.Messaging.Contracts;

public interface IEventPublisher
{
    Task PublishMessage<T>(T msg);
}
