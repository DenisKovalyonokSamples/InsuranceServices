using DK.PolicyService.Messaging.Contracts;
using NHibernate;

namespace DK.PolicyService.Messaging.RabbitMq.Outbox;

public class OutboxEventPublisher : IEventPublisher
{
    private readonly NHibernate.ISession session;

    public OutboxEventPublisher(NHibernate.ISession session)
    {
        this.session = session;
    }

    public async Task PublishMessage<T>(T msg)
    {
        await session.SaveAsync(new Message(msg));
    }
}
