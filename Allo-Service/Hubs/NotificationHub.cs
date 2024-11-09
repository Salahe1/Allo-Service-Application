using Allo_Service.Models;
using Microsoft.AspNetCore.SignalR;


public class NotificationHub : Hub
{
    public async Task JoinGroup(string userType)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userType);
    }

    public async Task SendNotification(Notification notification)
    {
        await Clients.Group(notification.UserType).SendAsync("ReceiveNotification", notification);
    }

    public async Task SendnewnotificationAsync(Notification newnotification)
    {
        await Clients.All.SendAsync("ReceiveNotification", newnotification);
    }
}