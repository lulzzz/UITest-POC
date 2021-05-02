using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Infrastructure
{
   public class BiddingRoundHub : Hub
   {
      public async Task SendMessageAsync(string message)
      {
         await Clients.All.SendAsync("ReceiveMessage", message);
      }
   }
}
