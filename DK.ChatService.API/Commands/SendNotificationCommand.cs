using MediatR;

namespace DK.ChatService.API.Commands;

public class SendNotificationCommand : IRequest<Unit>
{
    public string Message { get; set; }
}

