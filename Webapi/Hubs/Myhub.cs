using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs
{
    public class Myhub : Hub
    {
        //SendMessageAsync metodu tetiklendiğinde, gelen mesajı receiveMessage dinleyen tüm clientlara mesajı gönderir
        public async Task SendMessageAsync(string message) 
        {
            await Clients.All.SendAsync("receiveMessage",message);
        }
    }
}
