using Microsoft.AspNetCore.SignalR;

namespace DK.ChatService;

public class NameUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.Identity?.Name;
    }
}
